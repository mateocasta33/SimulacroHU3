namespace management.Application.DTOs;

public class AuthResponseDto
{
    public string UserName { get; set; }
    public string Token { get; set; }
    public string Role { get; set; }
    public DateTime Expiration { get; set; }
}

public class RegisterDto
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; } = "User";
}

public class loginDto
{
    public string Password { get; set; }
    public string Email { get; set; }
}