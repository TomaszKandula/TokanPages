using TokanPages.Backend.Domain.Entities.Invoicing;
using TokanPages.Backend.Domain.Entities.Users;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Persistence.DataAccess.Repositories.Invoicing.Models;

namespace TokanPages.Persistence.DataAccess.Repositories.Invoicing;

public interface IInvoicingRepository
{
    Task<List<UserCompany>> GetUserCompanies(HashSet<Guid> userIds);

    Task<List<UserBankAccount>> GetUserBankAccounts(HashSet<Guid> userIds);

    Task<List<BatchInvoice>> GetBatchInvoicesByIds(HashSet<Guid> ids);

    Task<List<BatchInvoiceItem>> GetBatchInvoiceItemsByIds(HashSet<Guid> ids);

    Task<List<VatNumberPattern>> GetVatNumberPatterns();

    Task<List<InvoiceTemplate>> GetInvoiceTemplates(bool isDeleted = false);

    Task<List<InvoiceTemplate>> GetInvoiceTemplatesByNames(HashSet<string> names);

    Task<InvoiceTemplate> GetInvoiceTemplate(Guid templateId, bool isDeleted = false);

    Task<Guid> CreateInvoiceTemplate(InvoiceTemplateDto data);

    Task UpdateInvoiceTemplate(Guid templateId, InvoiceTemplateDataDto data);

    Task RemoveInvoiceTemplate(Guid templateId);

    Task<BatchInvoiceProcessing?> GetBatchInvoiceProcessingByKey(Guid processBatchKey);

    Task<List<BatchInvoiceProcessing>> GetBatchInvoiceProcessingByStatus(ProcessingStatus status);

    Task<Guid> CreateBatchInvoiceProcessing();

    Task UpdateBatchInvoiceProcessingById(BatchInvoiceProcessingDto data);

    Task CreateBatchInvoice(List<BatchInvoiceDto> data);

    Task CreateBatchInvoiceItem(List<BatchInvoiceItemDto> data);

    Task<InvoiceDataDto?> GetIssuedInvoiceById(string invoiceNumber);

    Task CreateIssuedInvoice(List<IssuedInvoiceDto> data);
}