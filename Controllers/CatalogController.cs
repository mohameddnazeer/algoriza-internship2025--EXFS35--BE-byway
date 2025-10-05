using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Byway.Api.Services;
using Byway.Api.Data;
using Byway.Api.Models.DTOs;

namespace Byway.Api.Controllers;

[ApiController]
[Route("api/v1/catalog")]
public class CatalogController : ControllerBase
{
    private readonly CatalogService _catalog;
    private readonly BywayDbContext _context;
    
    public CatalogController(CatalogService catalog, BywayDbContext context) 
    { 
        _catalog = catalog; 
        _context = context;
    }

    [HttpGet("categories")]
    public IActionResult Categories() => Ok(_catalog.Categories);

    [HttpGet("instructors")]
    public async Task<IActionResult> Instructors([FromQuery] string? search = null, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var query = _context.Instructors.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(i => i.Name.Contains(search) || i.Bio.Contains(search));
        }

        var total = await query.CountAsync();
        var items = await query
            .OrderBy(i => i.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(i => new InstructorDto
            {
                Id = i.Id,
                Name = i.Name,
                Bio = i.Bio,
                JobTitle = i.JobTitle,
                AvatarBase64 = i.AvatarBase64,
                Email = i.Email,
                PhoneNumber = i.PhoneNumber,
                CoursesCount = i.Courses.Count(),
                CreatedAt = i.CreatedAt
            })
            .ToListAsync();

        return Ok(new { items, page, pageSize, totalItems = total, totalPages = (int)Math.Ceiling(total / (double)pageSize) });
    }

    [HttpGet("courses")]
    public IActionResult Courses([FromQuery] string? search = null, [FromQuery] int? categoryId = null,
                                 [FromQuery] string? sort = null, [FromQuery] string? order = "desc",
                                 [FromQuery] int page = 1, [FromQuery] int pageSize = 12)
    {
        var q = _catalog.Courses.AsEnumerable();
        if (!string.IsNullOrWhiteSpace(search))
            q = q.Where(c => c.Name.Contains(search, StringComparison.OrdinalIgnoreCase));
        if (categoryId.HasValue)
            q = q.Where(c => c.CategoryId == categoryId.Value);

        q = (sort, order?.ToLower()) switch
        {
            ("price", "asc") => q.OrderBy(c => c.Price),
            ("price", "desc") => q.OrderByDescending(c => c.Price),
            ("rating", "asc") => q.OrderBy(c => c.Rating),
            ("rating", "desc") => q.OrderByDescending(c => c.Rating),
            _ => q.OrderByDescending(c => c.Id)
        };

        var total = q.Count();
        var items = q.Skip((page - 1) * pageSize).Take(pageSize);
        return Ok(new { items, page, pageSize, totalItems = total, totalPages = (int)Math.Ceiling(total / (double)pageSize) });
    }

    [HttpGet("courses/{id:int}")]
    public IActionResult Course(int id)
    {
        var c = _catalog.Courses.FirstOrDefault(c => c.Id == id);
        if (c is null) return NotFound();
        return Ok(c);
    }

    [HttpGet("courses/{id:int}/more-like-this")]
    public IActionResult MoreLikeThis(int id) => Ok(_catalog.MoreLikeThis(id));

    [HttpGet("courses/top")]
    public IActionResult CoursesTop() => Ok(_catalog.Courses.Take(4));

    [HttpGet("categories/top")]
    public IActionResult CategoriesTop() => Ok(_catalog.Categories.Take(4));

    [HttpGet("instructors/top")]
    public async Task<IActionResult> InstructorsTop() 
    {
        var instructors = await _context.Instructors
            .OrderBy(i => i.Name)
            .Take(4)
            .Select(i => new InstructorDto
            {
                Id = i.Id,
                Name = i.Name,
                Bio = i.Bio,
                JobTitle = i.JobTitle,
                AvatarBase64 = i.AvatarBase64,
                Email = i.Email,
                PhoneNumber = i.PhoneNumber,
                CoursesCount = i.Courses.Count(),
                CreatedAt = i.CreatedAt
            })
            .ToListAsync();
            
        return Ok(instructors);
    }
}
