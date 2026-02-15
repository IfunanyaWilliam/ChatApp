using ChatApp.DTOs;
using ChatApp.Models;
using ChatApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Email) || 
            string.IsNullOrWhiteSpace(request.FirstName) || 
            string.IsNullOrWhiteSpace(request.LastName))
        {
            return BadRequest(new { message = "All fields are required" });
        }

        var user = _userService.RegisterUser(request.Email, request.FirstName, request.LastName);
        
        if (user == null)
        {
            return Conflict(new { message = "User with this email already exists" });
        }

        return Ok(user);
    }

    [HttpGet("online")]
    public IActionResult GetOnlineUsers()
    {
        var users = _userService.GetOnlineUsers();
        return Ok(users);
    }

    [HttpGet("{userId}")]
    public IActionResult GetUser(string userId)
    {
        var user = _userService.GetUser(userId);
        
        if (user == null)
        {
            return NotFound(new { message = "User not found" });
        }

        return Ok(new
        {
            userId = user.UserId,
            email = user.Email,
            firstName = user.FirstName,
            lastName = user.LastName,
            isOnline = user.IsOnline
        });
    }
}

