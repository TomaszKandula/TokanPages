using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Domain.Entities.Invoicing;

[ExcludeFromCodeCoverage]
public class IssuedInvoices : Entity<Guid>
{
    [Required]
    public Guid UserId { get; set; }

    [Required]
    [MaxLength(255)]
    public string InvoiceNumber { get; set; }

    [Required]
    public byte[] InvoiceData { get; set; }

    [Required]
    [MaxLength(100)]
    public string ContentType { get; set; }

    [Required]
    public DateTime GeneratedAt { get; set; }

    public Users User { get; set; }
}