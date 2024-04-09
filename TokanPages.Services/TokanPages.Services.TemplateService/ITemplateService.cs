using TokanPages.Services.TemplateService.Models;

namespace TokanPages.Services.TemplateService;

public interface ITemplateService
{
    Task<IEnumerable<InvoiceTemplateInfo>> GetInvoiceTemplates(CancellationToken cancellationToken = default);

    Task<InvoiceTemplateData> GetInvoiceTemplate(Guid templateId, CancellationToken cancellationToken = default);

    Task RemoveInvoiceTemplate(Guid templateId, CancellationToken cancellationToken = default);

    Task ReplaceInvoiceTemplate(Guid templateId, InvoiceTemplateData invoiceTemplateData, CancellationToken cancellationToken = default);

    Task<Guid> AddInvoiceTemplate(InvoiceTemplate invoiceTemplate, CancellationToken cancellationToken = default);
}