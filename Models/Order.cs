using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Byway.Api.Models;

public enum OrderStatus { Pending, Completed, Failed, Cancelled }

public class Order
{
    public int Id { get; set; }
    
    [Required]
    public int UserId { get; set; }
    
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Subtotal { get; set; }
    
    [Required]
    [Column(TypeName = "decimal(5,4)")]
    public decimal TaxRate { get; set; } = 0.15m; // 15% tax rate
    
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Total { get; set; }
    
    [Required]
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    // Navigation properties
    [ForeignKey("UserId")]
    public virtual User User { get; set; } = null!;
    
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public virtual Payment? Payment { get; set; }
}
