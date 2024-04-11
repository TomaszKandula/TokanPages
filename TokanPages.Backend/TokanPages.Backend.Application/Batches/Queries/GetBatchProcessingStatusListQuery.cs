using System.Diagnostics.CodeAnalysis;
using MediatR;

namespace TokanPages.Backend.Application.Batches.Queries;

[ExcludeFromCodeCoverage]
public class GetBatchProcessingStatusListQuery : IRequest<IEnumerable<GetBatchProcessingStatusListQueryResult>>
{
    public string FilterBy { get; set; } = "";
}