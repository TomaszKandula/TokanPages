using Microsoft.AspNetCore.Mvc;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Persistence.DataAccess.Repositories.Invoicing;

namespace TokanPages.Backend.Application.Invoicing.Batches.Queries;

public class GetIssuedBatchInvoiceQueryHandler : RequestHandler<GetIssuedBatchInvoiceQuery, FileContentResult>
{
    private readonly IInvoicingRepository _invoicingRepository;

    public GetIssuedBatchInvoiceQueryHandler(OperationDbContext operationDbContext, ILoggerService loggerService, IInvoicingRepository invoicingRepository) 
        : base(operationDbContext, loggerService) => _invoicingRepository = invoicingRepository;

    public override async Task<FileContentResult> Handle(GetIssuedBatchInvoiceQuery request, CancellationToken cancellationToken)
    {
        var data = await _invoicingRepository.GetIssuedInvoiceById(request.InvoiceNumber);
        if (data is null)
            throw new BusinessException(nameof(ErrorCodes.INVALID_INVOICE_NUMBER), ErrorCodes.INVALID_INVOICE_NUMBER);

        LoggerService.LogInformation($"Returned issued invoice. Invoice number: {data.Number}");
        return new FileContentResult(data.ContentData, data.ContentType);
    }
}