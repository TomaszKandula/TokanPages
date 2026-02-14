using TokanPages.Backend.Application.Invoicing.Models;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Repositories.Invoicing;

namespace TokanPages.Backend.Application.Invoicing.Templates.Queries;

public class GetInvoiceTemplatesQueryHandler : RequestHandler<GetInvoiceTemplatesQuery, IList<InvoiceTemplateInfo>>
{
    private readonly IInvoicingRepository _invoicingRepository;

    public GetInvoiceTemplatesQueryHandler(ILoggerService loggerService, IInvoicingRepository invoicingRepository) 
        : base(loggerService) => _invoicingRepository = invoicingRepository;

    public override async Task<IList<InvoiceTemplateInfo>> Handle(GetInvoiceTemplatesQuery request, CancellationToken cancellationToken)
    {
        var templates = await _invoicingRepository.GetInvoiceTemplates();
        var list = new List<InvoiceTemplateInfo>();
        foreach (var template in templates)
        {
            list.Add(new InvoiceTemplateInfo
            {
                Id = template.Id,
                Name = template.Name
            });
        }

        LoggerService.LogInformation($"Returned {templates.Count} invoice template(s)");
        return list;
    }
}