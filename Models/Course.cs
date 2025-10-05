using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Byway.Api.Models;

public enum Level { AllLevels, Beginner, Intermediate, Expert }

public class Course
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [StringLength(250)]
    public string Slug { get; set; } = string.Empty;
    
    [Required]
    public int CategoryId { get; set; }
    
    [Required]
    public int InstructorId { get; set; }
    
    [Required]
    public Level Level { get; set; }
    
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    [Range(0, 9999.99)]
    public decimal Price { get; set; }
    
    [Column(TypeName = "decimal(3,2)")]
    [Range(0, 5)]
    public decimal Rating { get; set; } = 0;
    
    public string? ThumbnailPath { get; set; }
    
    [Required]
    [StringLength(2000)]
    public string Description { get; set; } = string.Empty;
    
    [StringLength(5000)]
    public string? LongDescription { get; set; }
    
    public int Duration { get; set; } // Duration in minutes
    
    public int StudentsCount { get; set; } = 0;
    
    public bool IsActive { get; set; } = true;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    // Navigation properties
    [ForeignKey("CategoryId")]
    public virtual Category Category { get; set; } = null!;
    
    [ForeignKey("InstructorId")]
    public virtual Instructor Instructor { get; set; } = null!;
    
    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
