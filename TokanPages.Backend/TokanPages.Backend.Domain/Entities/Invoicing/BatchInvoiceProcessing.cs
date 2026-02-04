using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;
using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Backend.Domain.Entities.Invoicing;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "operation", TableName = "BatchInvoicesProcessing")]
public class BatchInvoiceProcessing : Entity<Guid>
{
    public TimeSpan? BatchProcessingTime { get; set; }
    [Required]
    public ProcessingStatus Status { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; }
}