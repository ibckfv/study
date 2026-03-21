namespace auth_validation_ibckfv.DTO;

public sealed class LoginRequest
{
    public string? UsernameOrEmail { get; set; }
    public string? Password { get; set; }
}