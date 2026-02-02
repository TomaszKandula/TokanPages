using TokanPages.Backend.Domain.Entities.Invoicing;

namespace TokanPages.Persistence.DataAccess.Repositories.Invoicing;

public interface IInvoicingRepository
{
    Task<List<VatNumberPattern>> GetVatNumberPatterns();

    Task<List<InvoiceTemplate>> GetInvoiceTemplates(bool isDeleted);
}