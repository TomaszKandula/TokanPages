using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Contracts;
using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Backend.Domain.Entities.User;

[ExcludeFromCodeCoverage]
public class UserSubscription : Entity<Guid>, IAuditable
{
    public Guid UserId { get; set; }
    public bool IsActive { get; set; }
    public bool AutoRenewal { get; set; }
    public TermType Term { get; set; }
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalAmount { get; set; }
    [Required]
    [MaxLength(3)]
    public string CurrencyIso { get; set; }
    [MaxLength(100)]
    public string ExtCustomerId { get; set; }
    [MaxLength(100)]
    public string ExtOrderId { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid? ModifiedBy { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public DateTime? ExpiresAt { get; set; }

    /* Navigation properties */
    public Users User { get; set; }
}