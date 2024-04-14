using TokanPages.Backend.Application.Templates.Queries.Models;

namespace TokanPages.Persistence.Caching.Abstractions;

/// <summary>
/// Templates cache contract.
/// </summary>
public interface ITemplatesCache
{
    /// <summary>
    /// Returns information of existing invoice templates.
    /// </summary>
    /// <param name="noCache">Enable/disable REDIS cache.</param>
    /// <returns>List of templates.</returns>
    Task<IList<InvoiceTemplateInfo>> GetInvoiceTemplates(bool noCache = false);
}