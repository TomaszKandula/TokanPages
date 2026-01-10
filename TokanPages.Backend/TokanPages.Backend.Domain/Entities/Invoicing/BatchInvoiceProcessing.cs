using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Backend.Domain.Entities.Invoicing;

[ExcludeFromCodeCoverage]
public class BatchInvoiceProcessing : Entity<Guid>
{
    public TimeSpan? BatchProcessingTime { get; set; }
    [Required]
    public ProcessingStatus Status { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; }

    /* Navigation properties */
    public ICollection<BatchInvoice> BatchInvoices { get; set; } = new HashSet<BatchInvoice>();
}