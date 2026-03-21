using System.Net;
using System.Text.Json;
using httplistener_test_ibckfv.Dto;
using httplistener_test_ibckfv.Services;

namespace httplistener_test_ibckfv.Helpers;

public static class ApiRouter
{
    public static async Task Route(HttpListenerContext ctx,
        AuthController auth,
        ApartmentController aptController,
        BookingController bookingController)
    {
        var req = ctx.Request;
        var res = ctx.Response;
        string path = req.Url!.AbsolutePath;
        string method = req.HttpMethod.ToUpperInvariant();

        if (path == "/auth/login" && method == "POST")
        {
            var body = await JsonHelper.ReadJsonBody<LoginRequest>(req);
            var sessionId = auth.Login(body!.Login!, body!.Password!);
            if (sessionId == null)
            {
                res.StatusCode = 401;
                await JsonHelper.WriteJson(res, new { error = "Invalid credentials" });
                return;
            }
            var cookie = new Cookie("SID", sessionId)
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddHours(8)
            };
            res.SetCookie(cookie);
            await JsonHelper.WriteJson(res, new { ok = true });
            return;
        }

        if (path.StartsWith("/apartments") && method == "GET")
        {
            var qs = req.QueryString;
            string city = qs["city"]!;
            string sort = qs["sort"]!; // asc desc
            var list = aptController.GetApartments(city, sort);
            await JsonHelper.WriteJson(res, list);
            return;
        }

        if (path == "/apartments" && method == "POST")
        {
            var currentUser = auth.GetUserFromRequest(req);
            if (currentUser == null)
            {
                res.StatusCode = 401; 
                await JsonHelper.WriteJson(res, new { error = "Unauthorized" }); 
                return;
            }
            var dto = await JsonHelper.ReadJsonBody<ApartmentCreateDto>(req);
            var created = aptController.CreateApartment(dto, currentUser.Id);
            await JsonHelper.WriteJson(res, created);
            return;
        }

        if (path == "/bookings" && method == "POST")
        {
            var currentUser = auth.GetUserFromRequest(req);
            if (currentUser == null)
            {
                res.StatusCode = 401; 
                await JsonHelper.WriteJson(res, new { error = "Unauthorized" }); 
                return;
            }
            var dto = await JsonHelper.ReadJsonBody<BookingCreateDto>(req);
            try
            {
                var booking = bookingController.CreateBooking(dto, currentUser.Id);
                await JsonHelper.WriteJson(res, booking);
            }
            catch (Exception e)
            {
                res.StatusCode = 400;
                await JsonHelper.WriteJson(res, new { error = e.Message });
            }
            return;
        }

        if (path.StartsWith("/bookings/") && method == "PUT")
        {
            var currentUser = auth.GetUserFromRequest(req);
            if (currentUser == null)
            {
                res.StatusCode = 401;
                await JsonHelper.WriteJson(res, new { error = "Unauthorized" });
                return;
            }
            var idStr = path.Substring("/bookings/".Length);
            if (!int.TryParse(idStr, out var bookingId))
            {
                res.StatusCode = 400; 
                await JsonHelper.WriteJson(res, new { error = "Bad id" }); 
                return;
            }
            var body = await JsonHelper.ReadJsonBody<BookingUpdateDto>(req);
            try
            {
                bookingController.UpdateBookingStatus(bookingId, body.Status, currentUser.Id);
                await JsonHelper.WriteJson(res, new { ok = true });
            }
            catch (Exception e)
            {
                res.StatusCode = 400;
                await JsonHelper.WriteJson(res, new { error = e.Message });
            }
            return;
        }

        if (path == "/reports/top" && method == "GET")
        {
            var list = bookingController.GetTop3ByApprovedBookings();
            await JsonHelper.WriteJson(res, list);
            return;
        }

        res.StatusCode = 404;
        await JsonHelper.WriteJson(res, new { error = "Not found" });
    }
}