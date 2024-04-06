using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Contracts;

namespace TokanPages.Backend.Domain.Entities.Invoicing;

[ExcludeFromCodeCoverage]
public class InvoiceTemplates : Entity<Guid>, ISoftDelete
{
    [Required]
    [MaxLength(255)]
    public string Name { get; set; }

    [Required]
    public byte[] Data { get; set; }

    [Required]
    [MaxLength(100)]
    public string ContentType { get; set; }

    [Required]
    [MaxLength(100)]
    public string ShortDescription { get; set; }

    [Required]
    public DateTime GeneratedAt { get; set; }

    [Required]
    public bool IsDeleted { get; set; }
}