using MediatR;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Persistence.DataAccess.Repositories.Invoicing;

namespace TokanPages.Backend.Application.Invoicing.Templates.Commands;

public class RemoveInvoiceTemplateCommandHandler : RequestHandler<RemoveInvoiceTemplateCommand, Unit>
{
    private readonly IInvoicingRepository _invoicingRepository;

    public RemoveInvoiceTemplateCommandHandler(OperationDbContext operationDbContext, ILoggerService loggerService, IInvoicingRepository invoicingRepository) 
        : base(operationDbContext, loggerService) => _invoicingRepository = invoicingRepository;

    public override async Task<Unit> Handle(RemoveInvoiceTemplateCommand request, CancellationToken cancellationToken)
    {
        await _invoicingRepository.RemoveInvoiceTemplate(request.Id);
        LoggerService.LogInformation($"Invoice template has been remove from the system. Invoice template ID: {request.Id}");
        return Unit.Value;
    }
}