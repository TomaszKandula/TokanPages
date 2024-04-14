using TokanPages.Backend.Application.Invoicing.Models;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;
using TokanPages.Services.TemplateService;

namespace TokanPages.Backend.Application.Invoicing.Templates.Queries;

public class GetInvoiceTemplatesQueryHandler : RequestHandler<GetInvoiceTemplatesQuery, IList<InvoiceTemplateInfo>>
{
    private readonly ITemplateService _templateService;

    public GetInvoiceTemplatesQueryHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        ITemplateService templateService) : base(databaseContext, loggerService) => _templateService = templateService;

    public override async Task<IList<InvoiceTemplateInfo>> Handle(GetInvoiceTemplatesQuery request, CancellationToken cancellationToken)
    {
        var result = await _templateService.GetInvoiceTemplates(cancellationToken);
        var templates = result as Services.TemplateService.Models.InvoiceTemplateInfo[] ?? result.ToArray();
        var list = new List<InvoiceTemplateInfo>();
        foreach (var template in templates)
        {
            list.Add(new InvoiceTemplateInfo
            {
                Id = template.Id,
                Name = template.Name
            });
        }

        LoggerService.LogInformation($"Returned {templates.Length} invoice template(s)");
        return list;
    }
}