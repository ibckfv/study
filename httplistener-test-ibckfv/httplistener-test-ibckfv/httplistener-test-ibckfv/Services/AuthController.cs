using System.Net;
using System.Security.Cryptography;
using System.Text;
using httplistener_test_ibckfv.models;
using httplistener_test_ibckfv.Repositories;

namespace httplistener_test_ibckfv.Services;

public class AuthController
{
    private readonly UserRepository _repo;
    private readonly SessionStore _sessions;

    public AuthController(UserRepository repo, SessionStore ss)
    {
        _repo = repo;
        _sessions = ss;
    }

    public string? Login(string login, string password)
    {
        var user = _repo.FindByLogin(login);
        if (user == null) return null;
        var passwordHash = Hash(password);
        if (passwordHash != user.PasswordHash) return null;
        var sid = _sessions.Create(user.Id);
        return sid;
    }

    public User? GetUserFromRequest(HttpListenerRequest req)
    {
        var cookie = req.Cookies["SID"];
        if (cookie == null) return null;
        var uid = _sessions.GetUserId(cookie.Value);
        if (!uid.HasValue) return null;
        return _repo.GetById(uid.Value);
    }

    public static string Hash(string s)
    {
        using var sha = SHA256.Create();
        var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(s));
        return Convert.ToHexString(bytes).ToLowerInvariant();
    }
}
