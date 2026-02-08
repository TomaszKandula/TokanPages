using MediatR;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Persistence.DataAccess.Repositories.Invoicing;
using TokanPages.Persistence.DataAccess.Repositories.Invoicing.Models;

namespace TokanPages.Backend.Application.Invoicing.Templates.Commands;

public class ReplaceInvoiceTemplateCommandHandler : RequestHandler<ReplaceInvoiceTemplateCommand, Unit>
{
    private readonly IInvoicingRepository _invoicingRepository;

    public ReplaceInvoiceTemplateCommandHandler(OperationDbContext operationDbContext, ILoggerService loggerService, 
        IInvoicingRepository invoicingRepository) : base(operationDbContext, loggerService) => _invoicingRepository = invoicingRepository;

    public override async Task<Unit> Handle(ReplaceInvoiceTemplateCommand request, CancellationToken cancellationToken)
    {
        var contentType = request.Data!.ContentType;
        var binary = request.Data.GetByteArray();

        var newTemplate = new InvoiceTemplateDataDto
        {
            ContentData = binary,
            ContentType = contentType,
            Description = request.Description
        };

        await _invoicingRepository.UpdateInvoiceTemplate(request.Id, newTemplate);
        LoggerService.LogInformation($"Invoice template (ID: {request.Id}) has been replaced by the provided template with description: {newTemplate.Description}");
        return Unit.Value;
    }
}