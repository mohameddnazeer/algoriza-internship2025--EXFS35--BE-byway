using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Byway.Api.Data;
using Byway.Api.Models;
using Byway.Api.Models.DTOs;
using BCrypt.Net;

namespace Byway.Api.Controllers;

[ApiController]
[Route("api/v1/users")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly BywayDbContext _context;

    public UserController(BywayDbContext context)
    {
        _context = context;
    }

    [HttpGet("profile")]
    public async Task<IActionResult> GetProfile()
    {
        var userId = GetCurrentUserId();
        if (userId == null)
            return Unauthorized();

        var user = await _context.Users.FindAsync(userId);
        if (user == null)
            return NotFound("User not found");

        return Ok(new
        {
            id = user.Id,
            email = user.Email,
            firstName = user.FirstName,
            lastName = user.LastName,
            isAdmin = user.IsAdmin,
            createdAt = user.CreatedAt,
            updatedAt = user.UpdatedAt
        });
    }

    [HttpPut("profile")]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileDto updateDto)
    {
        var userId = GetCurrentUserId();
        if (userId == null)
            return Unauthorized();

        var user = await _context.Users.FindAsync(userId);
        if (user == null)
            return NotFound("User not found");

        if (!string.IsNullOrEmpty(updateDto.FirstName))
            user.FirstName = updateDto.FirstName;

        if (!string.IsNullOrEmpty(updateDto.LastName))
            user.LastName = updateDto.LastName;

        if (!string.IsNullOrEmpty(updateDto.Email))
        {
            // Check if email is already taken by another user
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == updateDto.Email && u.Id != userId);
            if (existingUser != null)
                return BadRequest("Email is already taken");

            user.Email = updateDto.Email;
        }

        user.UpdatedAt = DateTime.UtcNow;

        try
        {
            await _context.SaveChangesAsync();
            return Ok(new { message = "Profile updated successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while updating profile" });
        }
    }

    [HttpPut("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
    {
        var userId = GetCurrentUserId();
        if (userId == null)
            return Unauthorized();

        var user = await _context.Users.FindAsync(userId);
        if (user == null)
            return NotFound("User not found");

        // Verify current password
        if (!BCrypt.Net.BCrypt.Verify(changePasswordDto.CurrentPassword, user.PasswordHash))
            return BadRequest("Current password is incorrect");

        // Update password
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(changePasswordDto.NewPassword);
        user.UpdatedAt = DateTime.UtcNow;

        try
        {
            await _context.SaveChangesAsync();
            return Ok(new { message = "Password changed successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while changing password" });
        }
    }

    // Admin endpoints
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllUsers([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var totalUsers = await _context.Users.CountAsync();
        var users = await _context.Users
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(u => new
            {
                id = u.Id,
                email = u.Email,
                firstName = u.FirstName,
                lastName = u.LastName,
                isAdmin = u.IsAdmin,
                createdAt = u.CreatedAt,
                updatedAt = u.UpdatedAt
            })
            .ToListAsync();

        return Ok(new
        {
            users = users,
            totalUsers = totalUsers,
            page = page,
            pageSize = pageSize,
            totalPages = (int)Math.Ceiling((double)totalUsers / pageSize)
        });
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
            return NotFound("User not found");

        return Ok(new
        {
            id = user.Id,
            email = user.Email,
            firstName = user.FirstName,
            lastName = user.LastName,
            isAdmin = user.IsAdmin,
            createdAt = user.CreatedAt,
            updatedAt = user.UpdatedAt
        });
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] AdminUpdateUserDto updateDto)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
            return NotFound("User not found");

        if (!string.IsNullOrEmpty(updateDto.FirstName))
            user.FirstName = updateDto.FirstName;

        if (!string.IsNullOrEmpty(updateDto.LastName))
            user.LastName = updateDto.LastName;

        if (!string.IsNullOrEmpty(updateDto.Email))
        {
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == updateDto.Email && u.Id != id);
            if (existingUser != null)
                return BadRequest("Email is already taken");

            user.Email = updateDto.Email;
        }

        if (updateDto.IsAdmin.HasValue)
            user.IsAdmin = updateDto.IsAdmin.Value;

        user.UpdatedAt = DateTime.UtcNow;

        try
        {
            await _context.SaveChangesAsync();
            return Ok(new { message = "User updated successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while updating user" });
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var currentUserId = GetCurrentUserId();
        if (currentUserId == id)
            return BadRequest("Cannot delete your own account");

        var user = await _context.Users.FindAsync(id);
        if (user == null)
            return NotFound("User not found");

        try
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return Ok(new { message = "User deleted successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while deleting user" });
        }
    }

    private int? GetCurrentUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return int.TryParse(userIdClaim, out var userId) ? userId : null;
    }
}