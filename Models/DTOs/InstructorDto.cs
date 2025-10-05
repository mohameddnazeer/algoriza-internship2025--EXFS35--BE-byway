using System.ComponentModel.DataAnnotations;

namespace Byway.Api.Models.DTOs;

public class InstructorDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public JobTitle JobTitle { get; set; }
    public string Bio { get; set; } = string.Empty;
    public string? AvatarBase64 { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public int CoursesCount { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateInstructorDto
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    public JobTitle JobTitle { get; set; }
    
    [Required]
    [StringLength(1000)]
    public string Bio { get; set; } = string.Empty;
    
    public string? AvatarBase64 { get; set; }
    
    [EmailAddress]
    [StringLength(255)]
    public string? Email { get; set; }
    
    [StringLength(20)]
    public string? PhoneNumber { get; set; }
}

public class UpdateInstructorDto
{
    [StringLength(100)]
    public string? Name { get; set; }
    
    public JobTitle? JobTitle { get; set; }
    
    [StringLength(1000)]
    public string? Bio { get; set; }
    
    public string? AvatarBase64 { get; set; }
    
    [EmailAddress]
    [StringLength(255)]
    public string? Email { get; set; }
    
    [StringLength(20)]
    public string? PhoneNumber { get; set; }
}