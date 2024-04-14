using MediatR;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;
using TokanPages.Services.TemplateService;

namespace TokanPages.Backend.Application.Invoicing.Templates.Commands;

public class RemoveInvoiceTemplateCommandHandler : RequestHandler<RemoveInvoiceTemplateCommand, Unit>
{
    private readonly ITemplateService _templateService;

    public RemoveInvoiceTemplateCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        ITemplateService templateService) : base(databaseContext, loggerService) => _templateService = templateService;

    public override async Task<Unit> Handle(RemoveInvoiceTemplateCommand request, CancellationToken cancellationToken)
    {
        await _templateService.RemoveInvoiceTemplate(request.Id, cancellationToken);
        LoggerService.LogInformation($"Invoice template has been remove from the system. Invoice template ID: {request.Id}");
        return Unit.Value;
    }
}