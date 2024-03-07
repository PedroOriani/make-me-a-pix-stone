using Microsoft.AspNetCore.Mvc;
using Pix.DTOs;
using Pix.Models;
using Pix.Services;

namespace Pix.Controllers;

[ApiController]
[Route("[Controller]")]

public class AuthController : ControllerBase
{
    private readonly UserService _userService;

    public AuthController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost("sign-up")]
    public IActionResult SignUp(CreateUserDTO dto)
    {
        User user = _userService.CreateUser(dto);
        return CreatedAtAction(null, null, user);
    }
}