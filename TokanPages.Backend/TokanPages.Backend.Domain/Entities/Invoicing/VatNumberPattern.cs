using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Domain.Entities.Invoicing;

[ExcludeFromCodeCoverage]
public class VatNumberPattern : Entity<Guid>
{
    [Required]
    [MaxLength(2)]
    public string CountryCode { get; set; }
    [Required]
    [MaxLength(100)]
    public string Pattern { get; set; }
}