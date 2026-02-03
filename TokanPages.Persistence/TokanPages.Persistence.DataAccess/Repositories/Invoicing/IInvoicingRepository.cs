using TokanPages.Backend.Domain.Entities.Invoicing;
using TokanPages.Persistence.DataAccess.Repositories.Invoicing.Models;

namespace TokanPages.Persistence.DataAccess.Repositories.Invoicing;

public interface IInvoicingRepository
{
    /// <summary>
    /// Returns VAT patterns for different languages.
    /// </summary>
    /// <returns>List of available VAT patterns.</returns>
    Task<List<VatNumberPattern>> GetVatNumberPatterns();

    /// <summary>
    /// Returns list of registered invoices templates with system.
    /// </summary>
    /// <param name="isDeleted">Delete flag.</param>
    /// <returns>List of templates.</returns>
    Task<List<InvoiceTemplate>> GetInvoiceTemplates(bool isDeleted);

    /// <summary>
    /// Returns registered invoice template by ID.
    /// </summary>
    /// <param name="templateId">Invoice template ID.</param>
    /// <param name="isDeleted">Delete flag.</param>
    /// <returns>Template file.</returns>
    Task<InvoiceTemplate> GetInvoiceTemplate(Guid templateId, bool isDeleted);

    /// <summary>
    /// Adds new invoice template to the database.
    /// </summary>
    /// <param name="template">Invoice template data.</param>
    /// <param name="generatedAt">Invoice timestamp.</param>
    /// <returns>Invoice template ID.</returns>
    Task<Guid> CreateInvoiceTemplate(InvoiceTemplateDto template, DateTime generatedAt);

    /// <summary>
    /// Updates current invoice template.
    /// </summary>
    /// <param name="templateId">Invoice template ID.</param>
    /// <param name="data">Holds Binary representation of a new invoice template and its content type.</param>
    Task<bool> ReplaceInvoiceTemplate(Guid templateId, InvoiceTemplateDataDto data);

    /// <summary>
    /// Remove (soft delete) existing invoice template.
    /// </summary>
    /// <param name="templateId">Invoice template ID.</param>
    Task<bool> RemoveInvoiceTemplate(Guid templateId);
}