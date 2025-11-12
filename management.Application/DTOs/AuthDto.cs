namespace management.Application.DTOs;

public class AuthResponseDto
{
    public string Token { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime Expiration { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}

public class RegisterDto
{
    public string UserName { get; set; }
    public DateTime Expiration { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; } = "User";
}

public class LoginDto
{
    public string Password { get; set; }
    public string Email { get; set; }
}

public class RefreshTokenDto
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}

public class RefreshTokenResponseDto
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public DateTime Expiration { get; set; }
}

public class RevokeTokenDto
{
    public string? Email { get; set; }
}