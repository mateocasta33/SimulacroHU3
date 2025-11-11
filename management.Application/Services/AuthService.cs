using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using management.Application.DTOs;
using management.Application.Interfaces;
using management.Domain.Entities;
using management.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace management.Application.Services;

public class AuthService : IAuthService
{
    private readonly IRepository<User> _repository;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapProfile;

    public AuthService(IRepository<User> repository, IConfiguration configuration, IMapper mapProfile)
    {
        _repository = repository;
        _configuration = configuration;
        _mapProfile = mapProfile;
    }
    
    public async Task<AuthResponseDto> register(RegisterDto registerDto)
    {
        if (registerDto == null)
            throw new ArgumentException("El cuerpo de la petición no puede estar vacío");

        if (string.IsNullOrWhiteSpace(registerDto.Password))
            throw new ArgumentException("La contraseña no puede estar vacía");

        var users = await _repository.GetAllAsync();
        var exist = users.Any(u => u.Email == registerDto.Email);

        if (exist)
            throw new ArgumentException("El usuario ya existe");

        // Mapeamos y creamos el usuario
        var user = _mapProfile.Map<User>(registerDto);
        user.CreatedAt = DateTime.Now;
        user.UpdatedAt = DateTime.Now;

        var createdUser = await _repository.CreateAsync(user);

        // Generamos el token aqui
        var tokenResponse = GenerateToken(createdUser);

        return tokenResponse;
    }


    
    public async Task<AuthResponseDto> login(LoginDto loginDto)
    {
        var users = await _repository.GetAllAsync();
        var user = users.FirstOrDefault(u => u.Email == loginDto.Email && u.IsActive);

        if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
            throw new UnauthorizedAccessException("Credenciales invalidas");

        return GenerateToken(user);
    }

    // Funcion para generar el Token
    public AuthResponseDto GenerateToken(User user)
    {
        // Obtenemos la clave secreta
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _configuration["Jwt:Key"] ?? throw new InvalidOperationException(" Jwt Key no configurado")));

        // Creamos las credenciales de la firma
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // Definimos los Claims/info del usuario
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim(ClaimTypes.Name, user.UserName) 
        };
        
        // Creamos el Token
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(2),
            signingCredentials: credentials
            );
        
        // Serializamos el token a string para enviarlo al cliente
        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return new AuthResponseDto
        {
            UserName = user.UserName,
            Role = user.Role,
            Expiration = token.ValidTo,
            Token = tokenString,
        };
    }
}