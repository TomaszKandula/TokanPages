using Microsoft.AspNetCore.Mvc;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;
using TokanPages.Services.BatchService;

namespace TokanPages.Backend.Application.Batches.Queries;

public class GetIssuedBatchInvoiceQueryHandler : RequestHandler<GetIssuedBatchInvoiceQuery, FileContentResult>
{
    private readonly IBatchService _batchService;

    public GetIssuedBatchInvoiceQueryHandler(DatabaseContext databaseContext, ILoggerService loggerService, IBatchService batchService) 
        : base(databaseContext, loggerService) => _batchService = batchService;

    public override async Task<FileContentResult> Handle(GetIssuedBatchInvoiceQuery request, CancellationToken cancellationToken)
    {
        var result = await _batchService.GetIssuedInvoice(request.InvoiceNumber, cancellationToken);
        LoggerService.LogInformation($"Returned issued invoice. Invoice number: {result.Number}");
        return new FileContentResult(result.ContentData, result.ContentType);
    }
}