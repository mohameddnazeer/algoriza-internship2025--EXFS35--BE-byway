using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Byway.Api.Data;
using Byway.Api.Models;
using Byway.Api.Models.DTOs;

namespace Byway.Api.Controllers;

[ApiController]
[Route("api/v1/courses")]
public class CourseController : ControllerBase
{
    private readonly BywayDbContext _context;

    public CourseController(BywayDbContext context)
    {
        _context = context;
    }

    private string GenerateSlug(string name)
    {
        // Convert to lowercase and replace spaces with hyphens
        var slug = name.ToLowerInvariant()
            .Replace(" ", "-")
            .Replace("&", "and")
            .Replace("'", "")
            .Replace("\"", "");
        
        // Remove any characters that aren't letters, numbers, or hyphens
        slug = System.Text.RegularExpressions.Regex.Replace(slug, @"[^a-z0-9\-]", "");
        
        // Remove multiple consecutive hyphens
        slug = System.Text.RegularExpressions.Regex.Replace(slug, @"-+", "-");
        
        // Remove leading and trailing hyphens
        slug = slug.Trim('-');
        
        return slug;
    }

    [HttpGet]
    public async Task<IActionResult> GetCourses(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 12,
        [FromQuery] string? search = null,
        [FromQuery] int? categoryId = null,
        [FromQuery] int? instructorId = null,
        [FromQuery] string? level = null,
        [FromQuery] decimal? minPrice = null,
        [FromQuery] decimal? maxPrice = null,
        [FromQuery] string? sortBy = "createdAt",
        [FromQuery] string? sortOrder = "desc")
    {
        var query = _context.Courses
            .Include(c => c.Category)
            .Include(c => c.Instructor)
            .AsQueryable();

        // Apply filters
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(c => c.Name.Contains(search) || c.Description.Contains(search));
        }

        if (categoryId.HasValue)
        {
            query = query.Where(c => c.CategoryId == categoryId.Value);
        }

        if (instructorId.HasValue)
        {
            query = query.Where(c => c.InstructorId == instructorId.Value);
        }

        if (!string.IsNullOrEmpty(level) && Enum.TryParse<Level>(level, out var levelEnum))
        {
            query = query.Where(c => c.Level == levelEnum);
        }

        if (minPrice.HasValue)
        {
            query = query.Where(c => c.Price >= minPrice.Value);
        }

        if (maxPrice.HasValue)
        {
            query = query.Where(c => c.Price <= maxPrice.Value);
        }

        // Apply sorting
        query = sortBy?.ToLower() switch
        {
            "name" => sortOrder?.ToLower() == "asc" ? query.OrderBy(c => c.Name) : query.OrderByDescending(c => c.Name),
            "price" => sortOrder?.ToLower() == "asc" ? query.OrderBy(c => c.Price) : query.OrderByDescending(c => c.Price),
            "rating" => sortOrder?.ToLower() == "asc" ? query.OrderBy(c => c.Rating) : query.OrderByDescending(c => c.Rating),
            "createdat" => sortOrder?.ToLower() == "asc" ? query.OrderBy(c => c.CreatedAt) : query.OrderByDescending(c => c.CreatedAt),
            _ => query.OrderByDescending(c => c.CreatedAt)
        };

        var totalCourses = await query.CountAsync();
        var courses = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var courseDtos = courses.Select(c => new CourseDto
        {
            Id = c.Id,
            Name = c.Name,
            Slug = c.Slug,
            CategoryId = c.CategoryId,
            CategoryName = c.Category.Name,
            InstructorId = c.InstructorId,
            InstructorName = c.Instructor.Name,
            Level = c.Level,
            Price = c.Price,
            Rating = c.Rating,
            ThumbnailPath = c.ThumbnailPath,
            Description = c.Description,
            LongDescription = c.LongDescription,
            Duration = c.Duration,
            StudentsCount = c.StudentsCount,
            IsActive = c.IsActive,
            CreatedAt = c.CreatedAt
        }).ToList();

        return Ok(new
        {
            courses = courseDtos,
            totalCourses = totalCourses,
            page = page,
            pageSize = pageSize,
            totalPages = (int)Math.Ceiling((double)totalCourses / pageSize)
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCourse(int id)
    {
        var course = await _context.Courses
            .Include(c => c.Category)
            .Include(c => c.Instructor)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (course == null)
            return NotFound("Course not found");

        var courseDto = new CourseDto
        {
            Id = course.Id,
            Name = course.Name,
            Slug = course.Slug,
            CategoryId = course.CategoryId,
            CategoryName = course.Category.Name,
            InstructorId = course.InstructorId,
            InstructorName = course.Instructor.Name,
            Level = course.Level,
            Price = course.Price,
            Rating = course.Rating,
            ThumbnailPath = course.ThumbnailPath,
            Description = course.Description,
            LongDescription = course.LongDescription,
            Duration = course.Duration,
            StudentsCount = course.StudentsCount,
            IsActive = course.IsActive,
            CreatedAt = course.CreatedAt
        };

        return Ok(courseDto);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Instructor")]
    public async Task<IActionResult> CreateCourse([FromBody] CreateCourseDto createDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Verify category exists
        var category = await _context.Categories.FindAsync(createDto.CategoryId);
        if (category == null)
            return BadRequest("Invalid category ID");

        // Verify instructor exists
        var instructor = await _context.Instructors.FindAsync(createDto.InstructorId);
        if (instructor == null)
            return BadRequest("Invalid instructor ID");

        // Generate slug from course name
        var slug = GenerateSlug(createDto.Name);
        
        var course = new Course
        {
            Name = createDto.Name,
            Slug = slug,
            Description = createDto.Description,
            Price = createDto.Price,
            Level = createDto.Level,
            Duration = createDto.Duration,
            CategoryId = createDto.CategoryId,
            InstructorId = createDto.InstructorId,
            ThumbnailPath = createDto.ThumbnailPath,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        try
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCourse), new { id = course.Id }, new { id = course.Id, message = "Course created successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while creating the course" });
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,Instructor")]
    public async Task<IActionResult> UpdateCourse(int id, [FromBody] UpdateCourseDto updateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var course = await _context.Courses.FindAsync(id);
        if (course == null)
            return NotFound("Course not found");

        // Verify category exists if provided
        if (updateDto.CategoryId.HasValue)
        {
            var category = await _context.Categories.FindAsync(updateDto.CategoryId.Value);
            if (category == null)
                return BadRequest("Invalid category ID");
            course.CategoryId = updateDto.CategoryId.Value;
        }

        // Verify instructor exists if provided
        if (updateDto.InstructorId.HasValue)
        {
            var instructor = await _context.Instructors.FindAsync(updateDto.InstructorId.Value);
            if (instructor == null)
                return BadRequest("Invalid instructor ID");
            course.InstructorId = updateDto.InstructorId.Value;
        }

        // Update fields
        if (!string.IsNullOrEmpty(updateDto.Name))
            course.Name = updateDto.Name;

        if (!string.IsNullOrEmpty(updateDto.Description))
            course.Description = updateDto.Description;

        if (updateDto.Price.HasValue)
            course.Price = updateDto.Price.Value;

        if (updateDto.Level.HasValue)
            course.Level = updateDto.Level.Value;

        if (updateDto.Duration.HasValue)
            course.Duration = updateDto.Duration.Value;

        if (!string.IsNullOrEmpty(updateDto.ThumbnailPath))
            course.ThumbnailPath = updateDto.ThumbnailPath;

        course.UpdatedAt = DateTime.UtcNow;

        try
        {
            await _context.SaveChangesAsync();
            return Ok(new { message = "Course updated successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while updating the course" });
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteCourse(int id)
    {
        var course = await _context.Courses.FindAsync(id);
        if (course == null)
            return NotFound("Course not found");

        try
        {
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Course deleted successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while deleting the course" });
        }
    }

    [HttpGet("categories")]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await _context.Categories
            .Select(c => new { id = c.Id, name = c.Name })
            .ToListAsync();

        return Ok(categories);
    }

    [HttpGet("instructors")]
    public async Task<IActionResult> GetInstructors()
    {
        var instructors = await _context.Instructors
            .Select(i => new { id = i.Id, name = i.Name })
            .ToListAsync();

        return Ok(instructors);
    }
}