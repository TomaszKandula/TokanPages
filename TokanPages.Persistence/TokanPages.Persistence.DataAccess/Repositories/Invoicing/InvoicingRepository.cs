using Microsoft.Extensions.Options;
using TokanPages.Backend.Configuration.Options;
using TokanPages.Backend.Domain.Entities.Invoicing;
using TokanPages.Persistence.DataAccess.Abstractions;

namespace TokanPages.Persistence.DataAccess.Repositories.Invoicing;

public class InvoicingRepository : RepositoryBase, IInvoicingRepository
{
    public InvoicingRepository(IDbOperations dbOperations, IOptions<AppSettingsModel> appSettings) 
        : base(dbOperations, appSettings) { }

    public async Task<List<VatNumberPattern>> GetVatNumberPatterns()
    {
        return (await DbOperations.Retrieve<VatNumberPattern>()).ToList();
    }

    public async Task<List<InvoiceTemplate>> GetInvoiceTemplates(bool isDeleted)
    {
        var filterBy = new { IsDeleted = isDeleted };
        return (await DbOperations.Retrieve<InvoiceTemplate>(filterBy: filterBy)).ToList();
    }
}