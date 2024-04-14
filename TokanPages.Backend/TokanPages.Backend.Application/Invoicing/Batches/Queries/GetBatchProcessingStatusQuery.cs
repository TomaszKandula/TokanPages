using MediatR;

namespace TokanPages.Backend.Application.Invoicing.Batches.Queries;

public class GetBatchProcessingStatusQuery : IRequest<GetBatchProcessingStatusQueryResult>
{
    public Guid ProcessBatchKey { get; set; }        
}