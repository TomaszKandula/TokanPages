using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Persistence.DataAccess.Repositories.Invoicing;
using TokanPages.Persistence.DataAccess.Repositories.Invoicing.Models;

namespace TokanPages.Backend.Application.Invoicing.Templates.Commands;

public class AddInvoiceTemplateCommandHandler : RequestHandler<AddInvoiceTemplateCommand, AddInvoiceTemplateCommandResult>
{
    private readonly IInvoicingRepository _invoicingRepository;
    
    private readonly IDateTimeService _dateTimeService;

    public AddInvoiceTemplateCommandHandler(OperationDbContext operationDbContext, ILoggerService loggerService, 
        IInvoicingRepository invoicingRepository, IDateTimeService dateTimeService) : base(operationDbContext, loggerService)
    {
        _invoicingRepository = invoicingRepository;
        _dateTimeService = dateTimeService;
    }

    public override async Task<AddInvoiceTemplateCommandResult> Handle(AddInvoiceTemplateCommand request, CancellationToken cancellationToken)
    {
        var fileName = request.Data!.FileName;
        var contentType = request.Data!.ContentType;
        var binary = request.Data.GetByteArray();

        var newInvoiceTemplate = new InvoiceTemplateDto
        {
            TemplateName = fileName,
            InvoiceTemplateData = new InvoiceTemplateDataDto
            {
                ContentData = binary,
                ContentType = contentType
            },
            InvoiceTemplateDescription = request.Description
        };

        var generatedAt = _dateTimeService.Now;
        var result = await _invoicingRepository.CreateInvoiceTemplate(newInvoiceTemplate, generatedAt);

        LoggerService.LogInformation($"New invoice template has been added. Invoice template name: {fileName}");

        return new AddInvoiceTemplateCommandResult { TemplateId = result };
    }
}