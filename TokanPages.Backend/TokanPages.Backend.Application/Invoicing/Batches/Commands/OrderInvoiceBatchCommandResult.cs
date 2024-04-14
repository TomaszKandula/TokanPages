using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Application.Invoicing.Batches.Commands;

[ExcludeFromCodeCoverage]
public class OrderInvoiceBatchCommandResult
{
    public Guid ProcessBatchKey { get; set; }
}