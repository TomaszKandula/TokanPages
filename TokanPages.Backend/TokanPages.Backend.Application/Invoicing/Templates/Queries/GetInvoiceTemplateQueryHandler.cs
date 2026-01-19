using Microsoft.AspNetCore.Mvc;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Services.TemplateService;

namespace TokanPages.Backend.Application.Invoicing.Templates.Queries;

public class GetInvoiceTemplateQueryHandler : RequestHandler<GetInvoiceTemplateQuery, FileContentResult>
{
    private readonly ITemplateService _templateService;

    public GetInvoiceTemplateQueryHandler(OperationDbContext operationDbContext, ILoggerService loggerService, 
        ITemplateService templateService) : base(operationDbContext, loggerService) => _templateService = templateService;

    public override async Task<FileContentResult> Handle(GetInvoiceTemplateQuery request, CancellationToken cancellationToken)
    {
        var result = await _templateService.GetInvoiceTemplate(request.Id, cancellationToken);
        LoggerService.LogInformation($"Returned invoice template. Description: {result.Description}");
        return new FileContentResult(result.ContentData, result.ContentType);
    }
}