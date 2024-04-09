using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Application.Batches.Queries;

[ExcludeFromCodeCoverage]
public class GetBatchProcessingStatusListQueryResult
{
    public int SystemCode { get; set; }

    public string ProcessingStatus { get; set; } = "";
}