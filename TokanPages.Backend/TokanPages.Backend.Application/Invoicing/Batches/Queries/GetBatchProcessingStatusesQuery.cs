using System.Diagnostics.CodeAnalysis;
using MediatR;

namespace TokanPages.Backend.Application.Invoicing.Batches.Queries;

[ExcludeFromCodeCoverage]
public class GetBatchProcessingStatusesQuery : IRequest<IEnumerable<GetBatchProcessingStatusesQueryResult>>
{
    public string FilterBy { get; set; } = "";
}