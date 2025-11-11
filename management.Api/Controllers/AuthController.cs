using management.Application.DTOs;
using management.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
            var register = await _authService.login(registerDto);
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
            var result = await _authService.register(registerDto);
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
}