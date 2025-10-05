using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Byway.Api.Data;
using Byway.Api.Models;
using Byway.Api.Models.DTOs;

namespace Byway.Api.Controllers;

[ApiController]
[Route("api/v1/instructors")]
public class InstructorController : ControllerBase
{
    private readonly BywayDbContext _context;

    public InstructorController(BywayDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetInstructors(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? search = null)
    {
        var query = _context.Instructors.AsQueryable();

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(i => i.Name.Contains(search) || i.Bio.Contains(search));
        }

        var totalInstructors = await query.CountAsync();
        var instructors = await query
            .Include(i => i.Courses)
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
                CoursesCount = i.Courses.Count,
                CreatedAt = i.CreatedAt
            })
            .ToListAsync();

        return Ok(new
        {
            instructors = instructors,
            totalInstructors = totalInstructors,
            page = page,
            pageSize = pageSize,
            totalPages = (int)Math.Ceiling((double)totalInstructors / pageSize)
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetInstructor(int id)
    {
        var instructor = await _context.Instructors
            .Include(i => i.Courses)
            .FirstOrDefaultAsync(i => i.Id == id);

        if (instructor == null)
            return NotFound("Instructor not found");

        var instructorDto = new InstructorDto
        {
            Id = instructor.Id,
            Name = instructor.Name,
            JobTitle = instructor.JobTitle,
            Bio = instructor.Bio,
            AvatarBase64 = instructor.AvatarBase64,
            Email = instructor.Email,
            PhoneNumber = instructor.PhoneNumber,
            CoursesCount = instructor.Courses.Count,
            CreatedAt = instructor.CreatedAt
        };

        var courses = instructor.Courses.Select(c => new
        {
            id = c.Id,
            title = c.Name,
            price = c.Price,
            rating = c.Rating,
            studentsCount = c.StudentsCount
        }).ToList();

        return Ok(new
        {
            instructor = instructorDto,
            courses = courses,
            totalCourses = courses.Count
        });
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateInstructor([FromBody] CreateInstructorDto createDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var instructor = new Instructor
        {
            Name = createDto.Name,
            Bio = createDto.Bio,
            JobTitle = createDto.JobTitle,
            Email = createDto.Email,
            PhoneNumber = createDto.PhoneNumber,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        try
        {
            _context.Instructors.Add(instructor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetInstructor), new { id = instructor.Id }, new { id = instructor.Id, message = "Instructor created successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while creating the instructor" });
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateInstructor(int id, [FromBody] UpdateInstructorDto updateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var instructor = await _context.Instructors.FindAsync(id);
        if (instructor == null)
            return NotFound("Instructor not found");
// Update fields
        if (!string.IsNullOrEmpty(updateDto.Name))
            instructor.Name = updateDto.Name;

        if (!string.IsNullOrEmpty(updateDto.Bio))
            instructor.Bio = updateDto.Bio;

        if (updateDto.JobTitle.HasValue)
            instructor.JobTitle = updateDto.JobTitle.Value;

        if (!string.IsNullOrEmpty(updateDto.Email))
            instructor.Email = updateDto.Email;

        if (!string.IsNullOrEmpty(updateDto.PhoneNumber))
            instructor.PhoneNumber = updateDto.PhoneNumber;

        instructor.UpdatedAt = DateTime.UtcNow;

        try
        {
            await _context.SaveChangesAsync();
            return Ok(new { message = "Instructor updated successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while updating the instructor" });
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteInstructor(int id)
    {
        var instructor = await _context.Instructors
            .Include(i => i.Courses)
            .FirstOrDefaultAsync(i => i.Id == id);

        if (instructor == null)
            return NotFound("Instructor not found");

        // Check if instructor has courses
        if (instructor.Courses.Any())
        {
            return BadRequest("Cannot delete instructor with existing courses. Please reassign or delete the courses first.");
        }

        try
        {
            _context.Instructors.Remove(instructor);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Instructor deleted successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while deleting the instructor" });
        }
    }
}