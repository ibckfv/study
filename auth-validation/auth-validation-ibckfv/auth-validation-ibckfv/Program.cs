using System.Net;
using System.Text;
using System.Text.Json;
using System.Security.Claims;
using auth_validation_ibckfv.DTO;
using auth_validation_ibckfv.helpers;
using auth_validation_ibckfv.models;

namespace auth_validation_ibckfv;

class Program
{
    async static Task Main()
    {
        var connectionString =
            "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=1469;SearchPath=public;";
        var db = new Database(connectionString);

        HttpListener listener = new HttpListener();
        listener.Prefixes.Add("http://localhost:7777/");
        listener.Start();

        while (listener.IsListening)
        {
            var context = await listener.GetContextAsync();
            _ = Task.Run(async () =>
            {
                var req = context.Request;
                var res = context.Response;
                string path = req.Url!.AbsolutePath.TrimStart('/');

                try
                {
                    if (path.StartsWith("api/"))
                    {
                        if (req.HttpMethod == "POST" && path == "api/register")
                        {
                            using var sr = new StreamReader(req.InputStream);
                            var body = await sr.ReadToEndAsync();
                            var reg = JsonSerializer.Deserialize<RegisterRequest>(body,
                                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                            var validator = new RegisterRequestValidator();
                            var validation = await validator.ValidateAsync(reg!);

                            if (!validation.IsValid)
                            {
                                res.StatusCode = 400;
                                await WriteJson(res, new
                                {
                                    ok = false,
                                    errors = validation.Errors.Select(e => e.ErrorMessage)
                                });
                            }

                            if (await db.GetUserByUsernameAsync(reg!.Username!) != null)
                            {
                                res.StatusCode = 400;
                                await WriteJson(res, new { ok = false, errors = new[] { "Имя пользователя занято" } });
                            }

                            if (await db.GetUserByEmailAsync(reg!.Email!) != null)
                            {
                                res.StatusCode = 400;
                                await WriteJson(res,
                                    new { ok = false, errors = new[] { "Пользователь с таким email уже существует" } });
                            }

                            var (hash, salt) = PasswordHasher.Hash(reg!.Password!);
                            int userId = await db.CreateUserWithHashAsync(reg.Username!, reg.Email!, reg.FirstName!,
                                reg.LastName!, reg.Phone, reg.Dob,
                                hash, salt, "user");
                            await WriteJson(res, new { ok = true, user_id = userId });
                        }
                        else if (req.HttpMethod == "POST" && path == "api/login")
                        {
                            using var sr = new StreamReader(req.InputStream);
                            var body = await sr.ReadToEndAsync();
                            var login = JsonSerializer.Deserialize<LoginRequest>(body,
                                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                            var validator = new LoginRequestValidator();
                            var validate = await validator.ValidateAsync(login!);

                            if (!validate.IsValid)
                            {
                                res.StatusCode = 400;
                                await WriteJson(res,
                                    new { ok = false, errors = validate.Errors.Select(e => e.ErrorMessage) });
                            }

                            User? user = await db.GetUserByUsernameAsync(login!.UsernameOrEmail!) ??
                                         await db.GetUserByEmailAsync(login!.UsernameOrEmail!);

                            if (user == null)
                            {
                                res.StatusCode = 401;
                                await WriteJson(res,
                                    new { ok = false, errors = "Неверные данные" });
                            }

                            if (!PasswordHasher.Validate(login!.Password!, user!.PasswordHash!, user!.PasswordSalt!))
                            {
                                res.StatusCode = 401;
                                await WriteJson(res, new { ok = false, errors = "Неверный пароль" });
                            }

                            string token =
                                JwtGenerator.GenerateJwtToken(user.UserId, user.Username!, user.Role ?? "user");
                            await WriteJson(res, new { ok = true, token });
                        }
                        else if (req.HttpMethod == "GET" && path == "api/userinfo")
                        {
                            var (isOk, principal, role) = AuthHelper.Authorize(req, res, "user");
                            if (!isOk)
                            {
                                res.StatusCode = 401;
                                await WriteJson(res, new { ok = false, errors = "Ошибка авторизации" });
                            }

                            int userId = int.Parse(principal!.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                            var user = await db.GetUserByIdAsync(userId);
                            await WriteJson(res, new { ok = true, user });
                        }
                        else if (req.HttpMethod == "GET" && path == "api/ads")
                        {
                            var ads = await db.GetAllAdsAsync();
                            await WriteJson(res, ads);
                        }
                        else if (req.HttpMethod == "POST" && path == "api/ads")
                        {
                            var (isOk, principal, role) = AuthHelper.Authorize(req, res, "user");
                            if (!isOk)
                            {
                                res.StatusCode = 401;
                                await WriteJson(res, new { ok = false, errors = "Ошибка авторизации" });
                            }

                            using var sr = new StreamReader(req.InputStream);
                            var body = await sr.ReadToEndAsync();
                            var ad = JsonSerializer.Deserialize<Ad>(body,
                                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                            if (ad == null || string.IsNullOrWhiteSpace(ad.Title))
                            {
                                res.StatusCode = 400;
                                await WriteJson(res, "Неправильное объявление");
                            }

                            int userId = int.Parse(principal!.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                            int id = await db.CreateAdAsync(ad!.Title!, ad.Description, ad.Price, userId);
                            await WriteJson(res, new { ok = true, ad_id = id });
                        }

                        //res.StatusCode = 404;
                        //await WriteText(res, "API не найден");
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(path))
                        {
                            path = "index.html";
                        }

                        var protectedPages = new Dictionary<string, string>
                        {
                            { "user.html", "user" },
                            { "admin.html", "admin" }
                        };
                        if (protectedPages.TryGetValue(path, out var reqiredRole))
                        {
                            var (isOk, _, _) = AuthHelper.Authorize(req, res, reqiredRole);
                            if (!isOk)
                            {
                                res.StatusCode = 401;
                                await WriteJson(res, new { ok = false, errors = "Непредвиденная ошибка" });
                            }
                        }

                        string filePath = Path.Combine("public", path);

                        if (File.Exists(filePath))
                        {
                            var contentType = GetContentType(filePath);
                            res.ContentType = contentType;
                            byte[] bytes = await File.ReadAllBytesAsync(filePath);
                            await res.OutputStream.WriteAsync(bytes);
                        }

                        //res.StatusCode = 404;
                        //await WriteText(res, "Not found");
                    }
                }
                catch (Exception ex)
                {
                    res.StatusCode = 500;
                    await WriteText(res, $"Exception: {ex.Message}");
                }
                finally
                {
                    res.OutputStream.Close();
                }
            });
        }
    }

    async static Task WriteJson(HttpListenerResponse res, object obj)
    {
        string json = JsonSerializer.Serialize(obj, new JsonSerializerOptions() { WriteIndented = true });
        byte[] bytes = Encoding.UTF8.GetBytes(json);
        await res.OutputStream.WriteAsync(bytes);
    }

    async static Task WriteText(HttpListenerResponse res, string text)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(text);
        await res.OutputStream.WriteAsync(bytes);
    }

    static string GetContentType(string file)
    {
        if (file.EndsWith(".html")) return "text/html; charset=utf-8";
        if (file.EndsWith(".css")) return "text/css";
        if (file.EndsWith(".js")) return "application/javascript";
        if (file.EndsWith(".png")) return "image/png";
        if (file.EndsWith(".jpg") || file.EndsWith(".jpeg")) return "image/jpeg";
        return "application/octet-stream";
    }
}