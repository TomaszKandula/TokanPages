using Microsoft.Extensions.Options;
using TokanPages.Backend.Configuration.Options;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Domain.Entities.Invoicing;
using TokanPages.Backend.Shared.Resources;
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

    public async Task<InvoiceTemplate> GetInvoiceTemplate(Guid templateId, bool isDeleted)
    {
        var filterBy = new { Id = templateId, IsDeleted = isDeleted };
        var data = (await DbOperations.Retrieve<InvoiceTemplate>(filterBy: filterBy)).SingleOrDefault();
        if (data is null)
            throw new BusinessException(nameof(ErrorCodes.INVALID_TEMPLATE_ID), ErrorCodes.INVALID_TEMPLATE_ID);            

        return data;
    }

    public async Task<bool> RemoveInvoiceTemplate(Guid templateId)
    {
        try
        {
            var updateBy = new { IsDeleted = true };
            var filterBy = new { Id = templateId };

            await DbOperations.Update<InvoiceTemplate>(updateBy, filterBy);
        }
        catch
        {
            return false;
        }

        return true;
    }
    
    
}