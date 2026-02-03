using Microsoft.AspNetCore.Mvc;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Persistence.DataAccess.Repositories.Invoicing;

namespace TokanPages.Backend.Application.Invoicing.Templates.Queries;

public class GetInvoiceTemplateQueryHandler : RequestHandler<GetInvoiceTemplateQuery, FileContentResult>
{
    private readonly IInvoicingRepository _invoicingRepository;

    public GetInvoiceTemplateQueryHandler(OperationDbContext operationDbContext, ILoggerService loggerService, 
        IInvoicingRepository invoicingRepository) : base(operationDbContext, loggerService) =>_invoicingRepository = invoicingRepository;

    public override async Task<FileContentResult> Handle(GetInvoiceTemplateQuery request, CancellationToken cancellationToken)
    {
        var result = await _invoicingRepository.GetInvoiceTemplate(request.Id);
        LoggerService.LogInformation($"Returned invoice template. Description: {result.ShortDescription}");
        return new FileContentResult(result.Data, result.ContentType);
    }
}