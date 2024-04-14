using MediatR;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;
using TokanPages.Services.TemplateService;
using TokanPages.Services.TemplateService.Models;

namespace TokanPages.Backend.Application.Invoicing.Templates.Commands;

public class ReplaceInvoiceTemplateCommandHandler : RequestHandler<ReplaceInvoiceTemplateCommand, Unit>
{
    private readonly ITemplateService _templateService;

    public ReplaceInvoiceTemplateCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        ITemplateService templateService) : base(databaseContext, loggerService) => _templateService = templateService;

    public override async Task<Unit> Handle(ReplaceInvoiceTemplateCommand request, CancellationToken cancellationToken)
    {
        var contentType = request.Data!.ContentType;
        var binary = request.Data.GetByteArray();

        var newTemplate = new InvoiceTemplateData
        {
            ContentData = binary,
            ContentType = contentType,
            Description = request.Description
        };

        await _templateService.ReplaceInvoiceTemplate(request.Id, newTemplate, cancellationToken);
        LoggerService.LogInformation($"Invoice template (ID: {request.Id}) has been replaced by the provided template with description: {newTemplate.Description}");
        return Unit.Value;
    }
}