using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Byway.Api.Data;
using Byway.Api.Models;
using Byway.Api.Models.DTOs;
using Byway.Api.Services;
using BCrypt.Net;

namespace Byway.Api.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class AuthController : ControllerBase
{
    private readonly BywayDbContext _context;
    private readonly JwtService _jwtService;

    public AuthController(BywayDbContext context, JwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Check if user already exists
        if (await _context.Users.AnyAsync(u => u.Email == registerDto.Email))
            return BadRequest("User with this email already exists");

        // Create new user
        var user = new User
        {
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            Email = registerDto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
            PhoneNumber = registerDto.PhoneNumber,
            IsAdmin = false,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Generate JWT token
        var token = _jwtService.GenerateToken(user);

        var userDto = new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            IsAdmin = user.IsAdmin
        };

        return Ok(new AuthResponseDto
        {
            Token = token,
            User = userDto
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Find user by email
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
        if (user == null)
            return BadRequest("Invalid email or password");

        // Verify password
        if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
            return BadRequest("Invalid email or password");

        // Generate JWT token
        var token = _jwtService.GenerateToken(user);

        var userDto = new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            IsAdmin = user.IsAdmin
        };

        return Ok(new AuthResponseDto
        {
            Token = token,
            User = userDto
        });
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken([FromHeader(Name = "Authorization")] string? authHeader)
    {
        if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
            return BadRequest("Invalid token format");

        var token = authHeader.Substring("Bearer ".Length).Trim();
        
        try
        {
            var principal = _jwtService.ValidateToken(token);
            var userIdClaim = principal.FindFirst("id")?.Value;
            
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
                return BadRequest("Invalid token");

            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return BadRequest("User not found");

            // Generate new token
            var newToken = _jwtService.GenerateToken(user);

            var userDto = new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                IsAdmin = user.IsAdmin
            };

            return Ok(new AuthResponseDto
            {
                Token = newToken,
                User = userDto
            });
        }
        catch
        {
            return BadRequest("Invalid token");
        }
    }
}
