using management.Application.DTOs;

namespace management.Application.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto> login(LoginDto loginDto);
    Task<AuthResponseDto> register(RegisterDto registerDto);
}