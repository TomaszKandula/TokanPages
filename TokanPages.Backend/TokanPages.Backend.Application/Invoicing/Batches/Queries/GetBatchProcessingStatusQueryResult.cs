using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Backend.Application.Invoicing.Batches.Queries;

[ExcludeFromCodeCoverage]
public class GetBatchProcessingStatusQueryResult
{
    public ProcessingStatus ProcessingStatus { get; set; }

    public TimeSpan? BatchProcessingTime { get; set; }

    public DateTime CreatedAt { get; set; }
}