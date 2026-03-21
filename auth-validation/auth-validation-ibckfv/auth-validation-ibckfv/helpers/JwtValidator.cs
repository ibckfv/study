using auth_validation_ibckfv.models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace auth_validation_ibckfv.helpers;

public static class JwtValidator
{
    private const string SecretKey = "super_secret_jwt_key_123456789_super_long_@_2025";

    public static (bool isValid, ClaimsPrincipal? principal) ValidateJwtToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParams = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "mydomain",
            ValidAudience = "mydomain",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey)),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromMinutes(2)
        };

        try
        {
            var principal = tokenHandler.ValidateToken(token, validationParams, out _);
            return (true, principal);
        }
        catch (SecurityTokenExpiredException)
        {
            return (false, null);
        }
        catch (SecurityTokenException)
        {
            return (false, null);
        }
        catch (Exception)
        {
            return (false, null);
        }
    }
}