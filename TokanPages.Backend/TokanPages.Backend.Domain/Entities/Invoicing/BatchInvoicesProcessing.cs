using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Backend.Domain.Entities.Invoicing;

[ExcludeFromCodeCoverage]
public class BatchInvoicesProcessing : Entity<Guid>
{
    public TimeSpan? BatchProcessingTime { get; set; }

    [Required]
    public ProcessingStatuses Status { get; set; }
        
    [Required]
    public DateTime CreatedAt { get; set; }

    public ICollection<BatchInvoices> BatchInvoices { get; set; } = new HashSet<BatchInvoices>();
}