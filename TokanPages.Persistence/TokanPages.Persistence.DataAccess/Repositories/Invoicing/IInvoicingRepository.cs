using TokanPages.Backend.Domain.Entities.Invoicing;

namespace TokanPages.Persistence.DataAccess.Repositories.Invoicing;

public interface IInvoicingRepository
{
    Task<List<VatNumberPattern>> GetVatNumberPatterns();

    Task<List<InvoiceTemplate>> GetInvoiceTemplates(bool isDeleted);

    Task<InvoiceTemplate> GetInvoiceTemplate(Guid templateId, bool isDeleted);

    Task<bool> RemoveInvoiceTemplate(Guid templateId);
}