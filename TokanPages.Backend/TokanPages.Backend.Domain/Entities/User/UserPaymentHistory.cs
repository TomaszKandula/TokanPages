using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Backend.Domain.Entities.User;

[ExcludeFromCodeCoverage]
public class UserPaymentHistory : Entity<Guid>
{
    public Guid UserId { get; set; }
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }
    [Required]
    [MaxLength(3)]
    public string CurrencyIso { get; set; }
    public TermType Term { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }

    /* Navigation properties */
    public Users Users { get; set; }
}