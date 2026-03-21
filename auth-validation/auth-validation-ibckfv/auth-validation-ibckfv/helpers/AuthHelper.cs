using System.Net;
using System.Security.Claims;
using auth_validation_ibckfv.models;

namespace auth_validation_ibckfv.helpers;

public static class AuthHelper
{
    public static (bool ok, ClaimsPrincipal? principal, string? role) Authorize(HttpListenerRequest req,
        HttpListenerResponse res,
        string requiredRole = "user")
    {
        string? auth = req.Headers["Authorization"];

        if (string.IsNullOrWhiteSpace(auth) || !auth.StartsWith("Bearer "))
        {
            res.StatusCode = 401;
            using var writer = new StreamWriter(res.OutputStream);
            writer.Write("Missing token");
            return (false, null, null);
        }

        var token = auth.Substring("Bearer ".Length).Trim();

        var (isValid, principal) = JwtValidator.ValidateJwtToken(token);

        if (!isValid || principal == null)
        {
            res.StatusCode = 401;
            using var writer = new StreamWriter(res.OutputStream);
            writer.Write("Invalid token");
            return (false, null, null);
        }

        var role = principal.FindFirst(ClaimTypes.Role)?.Value ?? "user";

        if (!HasRole(role, requiredRole))
        {
            res.StatusCode = 403;
            using var w = new StreamWriter(res.OutputStream);
            w.Write("Forbidden");
            return (false, principal, role);
        }

        return (true, principal, role);
    }

    private static bool HasRole(string actual, string required)
    {
        if (actual == "admin")
        {
            return true;
        }

        if (actual == "user" && (required == "user" || required == "guest"))
        {
            return true;
        }

        if (actual == "guest" && required == "guest")
        {
            return true;
        }
        return false;
    }
}