using Microsoft.Extensions.Options;
using TokanPages.Backend.Configuration.Options;
using TokanPages.Persistence.DataAccess.Abstractions;

namespace TokanPages.Persistence.DataAccess.Repositories.Invoicing;

public class InvoicingRepository : RepositoryBase, IInvoicingRepository
{
    public InvoicingRepository(IDbOperations dbOperations, IOptions<AppSettingsModel> appSettings) 
        : base(dbOperations, appSettings) { }
    
    
    
}