using System.ComponentModel.DataAnnotations;

namespace Byway.Api.Models;

public enum JobTitle { FullstackDeveloper, BackendDeveloper, FrontendDeveloper, UXUIDesigner }

public class Instructor
{
    public int Id { get; set; }
    
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
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    // Navigation properties
    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
