using System.ComponentModel.DataAnnotations;

namespace Byway.Api.Models.DTOs;

public class CourseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public int InstructorId { get; set; }
    public string InstructorName { get; set; } = string.Empty;
    public Level Level { get; set; }
    public decimal Price { get; set; }
    public decimal Rating { get; set; }
    public string? ThumbnailPath { get; set; }
    public string Description { get; set; } = string.Empty;
    public string? LongDescription { get; set; }
    public int Duration { get; set; }
    public int StudentsCount { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateCourseDto
{
    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    public int CategoryId { get; set; }
    
    [Required]
    public int InstructorId { get; set; }
    
    [Required]
    public Level Level { get; set; }
    
    [Required]
    [Range(0, 9999.99)]
    public decimal Price { get; set; }
    
    public string? ThumbnailPath { get; set; }
    
    [Required]
    [StringLength(2000)]
    public string Description { get; set; } = string.Empty;
    
    [StringLength(5000)]
    public string? LongDescription { get; set; }
    
    [Required]
    [Range(1, int.MaxValue)]
    public int Duration { get; set; }
}

public class UpdateCourseDto
{
    [StringLength(200)]
    public string? Name { get; set; }
    
    public int? CategoryId { get; set; }
    
    public int? InstructorId { get; set; }
    
    public Level? Level { get; set; }
    
    [Range(0, 9999.99)]
    public decimal? Price { get; set; }
    
    public string? ThumbnailPath { get; set; }
    
    [StringLength(2000)]
    public string? Description { get; set; }
    
    [StringLength(5000)]
    public string? LongDescription { get; set; }
    
    [Range(1, int.MaxValue)]
    public int? Duration { get; set; }
    
    public bool? IsActive { get; set; }
}