namespace management.Domain.Entities;

public class User
{
    public int Id { get; set; }
        
    public string UserName { get; set; } = string.Empty;
        
    public string Email { get; set; } = string.Empty;
        
    public string PasswordHash { get; set; } = string.Empty;
        
    public string Role { get; set; } = "User"; // "User" o "Admin"
    public DateTime? RefreshTokenExpiry { get; set; }
    public string? RefreshToken { get; set; }
        
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
    public DateTime? UpdatedAt { get; set; }
        
    public bool IsActive { get; set; } = true;
    
}