using Jonas.Darts.Backend.Services.Interfaces;
using Jonas.Darts.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Jonas.Darts.Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDTO dto)
    {
        var (Success, Message) = await _authService.RegisterAsync(dto);
        if (!Success)
            return BadRequest(Message);

        return Ok(Message);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO dto)
    {
        var result = await _authService.LoginAsync(dto);
        if (!result.Success)
            return Unauthorized(result.TokenOrMessage);

        return Ok(new LoginResponse(result.TokenOrMessage));
    }
}
