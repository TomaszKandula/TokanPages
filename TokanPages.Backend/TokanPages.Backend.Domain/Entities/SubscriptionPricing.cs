using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Contracts;
using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Backend.Domain.Entities;

[ExcludeFromCodeCoverage]
public class SubscriptionPricing : Entity<Guid>, IAuditable
{
    public TermType Term { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
    [Required]
    [MaxLength(3)]
    public string CurrencyIso { get; set; }
    [Required]
    [MaxLength(3)]
    public string LanguageIso { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid? ModifiedBy { get; set; }
    public DateTime? ModifiedAt { get; set; }
}