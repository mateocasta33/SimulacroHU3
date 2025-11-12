using management.Application.DTOs;
using management.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace management.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    // Api login
    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login(LoginDto registerDto)
    {
        try
        {
            var register = await _authService.LoginAsync(registerDto);
            return Ok(register);
        }
        catch (NullReferenceException e)
        {
            return BadRequest(new { message = e.Message });
        }
        catch (UnauthorizedAccessException e)
        {
            return Unauthorized(new { message = e.Message });
        }
        catch (Exception e)
        {
            return StatusCode(500, new { message = "Error interno del servidor", details = e.Message });
        }
    }
    
    // Api Register
    [HttpPost("register")]
    public async Task<ActionResult<AuthResponseDto>> Register(RegisterDto registerDto)
    {
        try
        {
            var result = await _authService.RegisterAsync(registerDto);
            return Ok(result);
        }
        catch (NullReferenceException e)
        {
            return BadRequest(new { message = e.Message });
        }
        catch (ArgumentNullException e)
        {
            return BadRequest(new { message = e.Message });
        }
        catch (Exception e)
        {
            return StatusCode(500, new { message = "Error interno del servidor", details = e.Message });
        }
    }

    [HttpPost]
    public async Task<ActionResult<AuthResponseDto>> RefreshToken(RefreshTokenDto refreshTokenDto)
    {
        try
        {
            var result = await _authService.RefreshTokenAsync(refreshTokenDto);
            return Ok(result);
        }
        catch (SecurityTokenException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, new { message = "Error interno del servidor", details = e.Message });
        }
    }

    [HttpPost]
    public async Task<ActionResult> RevokeToken(RevokeTokenDto revokeTokenDto)
    {
        try
        {
            var result = await _authService.RevokeTokenAsync(revokeTokenDto);
            return Ok(result);
        }
        catch (Exception e)
        {
            return StatusCode(500, new { message = "Error interno del servidor", details = e.Message });
        }
    }
}