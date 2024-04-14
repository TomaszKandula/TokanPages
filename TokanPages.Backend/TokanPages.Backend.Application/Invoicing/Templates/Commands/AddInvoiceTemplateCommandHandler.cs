using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;
using TokanPages.Services.TemplateService;
using TokanPages.Services.TemplateService.Models;

namespace TokanPages.Backend.Application.Invoicing.Templates.Commands;

public class AddInvoiceTemplateCommandHandler : RequestHandler<AddInvoiceTemplateCommand, AddInvoiceTemplateCommandResult>
{
    private readonly ITemplateService _templateService;

    public AddInvoiceTemplateCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        ITemplateService templateService) : base(databaseContext, loggerService) => _templateService = templateService;

    public override async Task<AddInvoiceTemplateCommandResult> Handle(AddInvoiceTemplateCommand request, CancellationToken cancellationToken)
    {
        var fileName = request.Data!.FileName;
        var contentType = request.Data!.ContentType;
        var binary = request.Data.GetByteArray();

        var newInvoiceTemplate = new InvoiceTemplate
        {
            TemplateName = fileName,
            InvoiceTemplateData = new InvoiceTemplateData
            {
                ContentData = binary,
                ContentType = contentType
            },
            InvoiceTemplateDescription = request.Description
        };

        var result = await _templateService.AddInvoiceTemplate(newInvoiceTemplate, cancellationToken);
        LoggerService.LogInformation($"New invoice template has been added. Invoice template name: {fileName}");

        return new AddInvoiceTemplateCommandResult { TemplateId = result };
    }
}