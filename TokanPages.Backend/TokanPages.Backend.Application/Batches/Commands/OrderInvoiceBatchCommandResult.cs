using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Application.Batches.Commands;

[ExcludeFromCodeCoverage]
public class OrderInvoiceBatchCommandResult
{
    public Guid ProcessBatchKey { get; set; }
}