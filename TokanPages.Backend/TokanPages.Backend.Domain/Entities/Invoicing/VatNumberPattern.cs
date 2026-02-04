using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;

namespace TokanPages.Backend.Domain.Entities.Invoicing;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "operation", TableName = "VatNumberPatterns")]
public class VatNumberPattern : Entity<Guid>
{
    [Required]
    [MaxLength(2)]
    public string CountryCode { get; set; }
    [Required]
    [MaxLength(100)]
    public string Pattern { get; set; }
}