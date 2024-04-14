using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Application.Invoicing.Batches.Queries;

[ExcludeFromCodeCoverage]
public class GetBatchProcessingStatusesQueryResult
{
    public int SystemCode { get; set; }

    public string ProcessingStatus { get; set; } = "";
}