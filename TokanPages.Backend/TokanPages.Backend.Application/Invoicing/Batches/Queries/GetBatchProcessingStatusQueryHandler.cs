using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Persistence.DataAccess.Repositories.Invoicing;

namespace TokanPages.Backend.Application.Invoicing.Batches.Queries;

public class GetBatchProcessingStatusQueryHandler : RequestHandler<GetBatchProcessingStatusQuery, GetBatchProcessingStatusQueryResult>
{
    private readonly IInvoicingRepository _invoicingRepository;

    public GetBatchProcessingStatusQueryHandler(OperationDbContext operationDbContext, ILoggerService loggerService, 
        IInvoicingRepository invoicingRepository) : base(operationDbContext, loggerService) => _invoicingRepository = invoicingRepository;

    public override async Task<GetBatchProcessingStatusQueryResult> Handle(GetBatchProcessingStatusQuery request, CancellationToken cancellationToken)
    {
        var data = await _invoicingRepository.GetBatchInvoiceProcessingByKey(request.ProcessBatchKey);
        if (data == null)
            throw new BusinessException(nameof(ErrorCodes.INVALID_PROCESSING_BATCH_KEY), ErrorCodes.INVALID_PROCESSING_BATCH_KEY);

        LoggerService.LogInformation($"Returned batch invoice processing status for key: {request.ProcessBatchKey}");

        return new GetBatchProcessingStatusQueryResult
        {
            ProcessingStatus = data.Status,
            BatchProcessingTime = data.BatchProcessingTime,
            CreatedAt = data.CreatedAt
        };
    }
}