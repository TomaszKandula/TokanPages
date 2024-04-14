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
        var newInvoiceTemplate = new InvoiceTemplate
        {
            TemplateName = request.Name,
            InvoiceTemplateData = new InvoiceTemplateData
            {
                ContentData = request.Data,
                ContentType = request.DataType
            },
            InvoiceTemplateDescription = request.Description
        };

        var result = await _templateService.AddInvoiceTemplate(newInvoiceTemplate, cancellationToken);
        LoggerService.LogInformation($"New invoice template has been added. Invoice template name: {request.Name}");

        return new AddInvoiceTemplateCommandResult { TemplateId = result };
    }
}