using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Byway.Api.Data;

namespace Byway.Api.Controllers;

[ApiController]
[Route("api/v1/admin")]
[Authorize(Roles = "Admin")]
public class AdminController : ControllerBase
{
    private readonly BywayDbContext _context;

    public AdminController(BywayDbContext context)
    {
        _context = context;
    }

    [HttpGet("dashboard/stats")]
    public async Task<IActionResult> GetDashboardStats()
    {
        var totalInstructors = await _context.Instructors.CountAsync();
        var totalCategories = await _context.Categories.CountAsync();
        var totalCourses = await _context.Courses.CountAsync();
        
        // For now, using a placeholder for monthly subscriptions
        // This would typically come from a subscriptions or orders table
        var monthlySubscriptions = 1234;

        return Ok(new
        {
            totalInstructors = totalInstructors,
            totalCategories = totalCategories,
            totalCourses = totalCourses,
            monthlySubscriptions = monthlySubscriptions
        });
    }
}