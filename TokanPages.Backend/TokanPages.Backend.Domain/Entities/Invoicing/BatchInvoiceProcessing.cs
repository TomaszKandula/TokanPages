using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;
using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Backend.Domain.Entities.Invoicing;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "operation", TableName = "BatchInvoicesProcessing")]
public class BatchInvoiceProcessing : Entity<Guid>
{
    public TimeSpan? BatchProcessingTime { get; set; }

    public required ProcessingStatus Status { get; set; }

    public required DateTime CreatedAt { get; set; }
}