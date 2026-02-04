using TokanPages.Backend.Domain.Entities.Invoicing;
using TokanPages.Backend.Domain.Entities.Users;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Persistence.DataAccess.Repositories.Invoicing.Models;

namespace TokanPages.Persistence.DataAccess.Repositories.Invoicing;

public interface IInvoicingRepository
{
    /// <summary>
    /// Returns list of user companies.
    /// </summary>
    /// <param name="userIds">List of user UID.</param>
    /// <returns>List of user companies.</returns>
    Task<List<UserCompany>> GetUserCompanies(HashSet<Guid> userIds);

    /// <summary>
    /// Returns list of user bank accounts.
    /// </summary>
    /// <param name="userIds">List of user UID.</param>
    /// <returns>List of user bank accounts.</returns>
    Task<List<UserBankAccount>> GetUserBankAccounts(HashSet<Guid> userIds);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    Task<List<BatchInvoice>> GetBatchInvoicesByIds(HashSet<Guid> ids);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    Task<List<BatchInvoiceItem>> GetBatchInvoiceItemsByIds(HashSet<Guid> ids);

    /// <summary>
    /// Returns VAT patterns for different languages.
    /// </summary>
    /// <returns>List of available VAT patterns.</returns>
    Task<List<VatNumberPattern>> GetVatNumberPatterns();

    /// <summary>
    /// Returns list of registered invoices templates with system.
    /// </summary>
    /// <param name="isDeleted">Optional delete flag.</param>
    /// <returns>List of templates.</returns>
    Task<List<InvoiceTemplate>> GetInvoiceTemplates(bool isDeleted = false);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="names"></param>
    /// <returns></returns>
    Task<List<InvoiceTemplate>> GetInvoiceTemplatesByNames(HashSet<string> names);

    /// <summary>
    /// Returns registered invoice template by ID.
    /// </summary>
    /// <param name="templateId">Invoice template ID.</param>
    /// <param name="isDeleted">Optional delete flag.</param>
    /// <returns>Template file.</returns>
    Task<InvoiceTemplate> GetInvoiceTemplate(Guid templateId, bool isDeleted = false);

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

    /// <summary>
    /// Returns batch invoice processing by given Key.
    /// </summary>
    /// <param name="processBatchKey">Mandatory processing key.</param>
    /// <returns>Entity.</returns>
    Task<BatchInvoiceProcessing?> GetBatchInvoiceProcessingByKey(Guid processBatchKey);

    /// <summary>
    /// Returns batch invoice processing by given Status:
    /// </summary>
    /// <list type="bullet">
    /// <item><description>new - newly created entry, processing is not started yet.</description></item>
    /// <item><description>started - indicates ongoing processing.</description></item>
    /// <item><description>finished - indicates successful processing.</description></item>
    /// <item><description>failed - indicates failed processing.</description></item>
    /// </list>
    /// <param name="status">Requested status.</param>
    /// <returns>List of entities.</returns>
    Task<List<BatchInvoiceProcessing>> GetBatchInvoiceProcessingByStatus(ProcessingStatus status);

    /// <summary>
    /// Creates a new batch invoice processing entry.
    /// </summary>
    /// <param name="createdAt">Timestamp.</param>
    /// <returns>Process UID.</returns>
    Task<Guid> CreateBatchInvoiceProcessing(DateTime createdAt);

    /// <summary>
    /// Updates current processing status entry by given ID.
    /// </summary>
    /// <remarks>
    /// Provided ID is used to filter the entity. The key (ID) remain unchanged.
    /// </remarks>
    /// <param name="data">Update details (id, time, status).</param>
    /// <returns>Indicates success.</returns>
    Task<bool> UpdateBatchInvoiceProcessingById(BatchInvoiceProcessingDto data);

    /// <summary>
    /// Creates a new batch invoice entry.
    /// </summary>
    /// <param name="data">Batch invoice details.</param>
    /// <returns>Process UID.</returns>
    Task<Guid> CreateBatchInvoice(BatchInvoiceDto data);

    /// <summary>
    /// Creates a new batch invoice item entry.
    /// </summary>
    /// <param name="data">Item details.</param>
    /// <returns></returns>
    Task<Guid> CreateBatchInvoiceItem(BatchInvoiceItemDto data);

    /// <summary>
    /// Returns issued invoice by given invoice number.
    /// </summary>
    /// <remarks>
    /// If nothing is found, returns null.
    /// </remarks>
    /// <param name="invoiceNumber">Invoice number.</param>
    /// <returns>File data</returns>
    Task<InvoiceDataDto?> GetIssuedInvoiceById(string invoiceNumber);

    /// <summary>
    /// Creates a new entry with processed invoice.
    /// </summary>
    /// <param name="userId">User ID.</param>
    /// <param name="invoiceNumber">Invoice number.</param>
    /// <param name="invoiceData">Invoice file binary data.</param>
    /// <returns>Invoice UID.</returns>
    Task<Guid> CreateIssuedInvoice(Guid userId, string invoiceNumber, byte[] invoiceData);
}