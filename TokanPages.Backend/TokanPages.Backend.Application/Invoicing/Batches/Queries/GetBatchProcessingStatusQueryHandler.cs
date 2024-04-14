using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;
using TokanPages.Services.BatchService;

namespace TokanPages.Backend.Application.Invoicing.Batches.Queries;

public class GetBatchProcessingStatusQueryHandler : RequestHandler<GetBatchProcessingStatusQuery, GetBatchProcessingStatusQueryResult>
{
    private readonly IBatchService _batchService;

    public GetBatchProcessingStatusQueryHandler(DatabaseContext databaseContext, ILoggerService loggerService, IBatchService batchService) 
        : base(databaseContext, loggerService) => _batchService = batchService;
        
    public override async Task<GetBatchProcessingStatusQueryResult> Handle(GetBatchProcessingStatusQuery request, CancellationToken cancellationToken)
    {
        var result = await _batchService.GetBatchInvoiceProcessingStatus(request.ProcessBatchKey, cancellationToken);
        LoggerService.LogInformation($"Returned batch invoice processing status. PBK: {request.ProcessBatchKey}");
        return new GetBatchProcessingStatusQueryResult
        {
            ProcessingStatus = result.Status,
            BatchProcessingTime = result.BatchProcessingTime,
            CreatedAt = result.CreatedAt
        };
    }
}