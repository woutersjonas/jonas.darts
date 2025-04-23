using Microsoft.AspNetCore.Mvc;
using Jonas.Darts.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Jonas.Darts.Backend.Services.Interfaces;

namespace Jonas.Darts.Backend.Controllers;

[Authorize(Roles = "Admin")]
[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IEnumerable<User>> GetUsers()
    {
        return await _userService.GetUsersAsync();
    }
}
