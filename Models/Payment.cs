using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Byway.Api.Models;

public enum PaymentStatus { Pending, Completed, Failed, Refunded }
public enum PaymentMethod { CreditCard, DebitCard, PayPal, BankTransfer }

public class Payment
{
    public int Id { get; set; }
    
    [Required]
    public int OrderId { get; set; }
    
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }
    
    [Required]
    public PaymentMethod PaymentMethod { get; set; }
    
    [Required]
    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
    
    [StringLength(100)]
    public string? TransactionId { get; set; }
    
    [StringLength(500)]
    public string? PaymentDetails { get; set; } // JSON string for additional payment info
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    // Navigation properties
    [ForeignKey("OrderId")]
    public virtual Order Order { get; set; } = null!;
}