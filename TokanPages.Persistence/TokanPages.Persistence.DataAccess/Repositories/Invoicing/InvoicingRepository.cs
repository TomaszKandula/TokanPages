using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Domain.Entities.Invoicing;
using TokanPages.Backend.Domain.Entities.Users;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Backend.Shared.Options;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Abstractions;
using TokanPages.Persistence.DataAccess.Repositories.Invoicing.Models;

namespace TokanPages.Persistence.DataAccess.Repositories.Invoicing;

public class InvoicingRepository : RepositoryBase, IInvoicingRepository
{
    private readonly IDateTimeService _dateTimeService;

    public InvoicingRepository(IDbOperations dbOperations, IOptions<AppSettingsModel> appSettings, 
        IDateTimeService dateTimeService) : base(dbOperations, appSettings) => _dateTimeService = dateTimeService;

    private static BusinessException InvalidTemplateId => new(nameof(ErrorCodes.INVALID_TEMPLATE_ID), ErrorCodes.INVALID_TEMPLATE_ID);

    private static BusinessException InvalidContentType => new(nameof(ErrorCodes.INVALID_CONTENT_TYPE), ErrorCodes.INVALID_CONTENT_TYPE);

    /// <inheritdoc/>
    public async Task<List<UserCompany>> GetUserCompanies(HashSet<Guid> userIds)
    {
        const string query = @"
            SELECT
                operation.UserCompanies.Id
                operation.UserCompanies.UserId,
                operation.UserCompanies.CompanyName,
                operation.UserCompanies.VatNumber,
                operation.UserCompanies.EmailAddress,
                operation.UserCompanies.PhoneNumber,
                operation.UserCompanies.StreetAddress,
                operation.UserCompanies.PostalCode,
                operation.UserCompanies.City,
                operation.UserCompanies.CurrencyCode,
                operation.UserCompanies.CountryCode              
            FROM
                operation.UserCompanies
            WHERE
                operation.UserCompanies.Id IN @UserIds
        ";

        await using var db = new SqlConnection(AppSettings.DbDatabaseContext);
        var parameters = new { UserIds = userIds };
        return (await db.QueryAsync<UserCompany>(query, parameters)).ToList();
    }

    /// <inheritdoc/>
    public async Task<List<UserBankAccount>> GetUserBankAccounts(HashSet<Guid> userIds)
    {
        const string query = @"
            SELECT
                operation.UserBankAccounts.Id
                operation.UserBankAccounts.UserId,
                operation.UserBankAccounts.BankName,
                operation.UserBankAccounts.SwiftNumber,
                operation.UserBankAccounts.AccountNumber,
                operation.UserBankAccounts.CurrencyCode
            FROM
                operation.UserBankAccounts
            WHERE
                operation.UserBankAccounts.Id IN @UserIds
        ";

        await using var db = new SqlConnection(AppSettings.DbDatabaseContext);
        var parameters = new { UserIds = userIds };
        return (await db.QueryAsync<UserBankAccount>(query, parameters)).ToList();
    }

    /// <inheritdoc/>
    public async Task<List<BatchInvoice>> GetBatchInvoicesByIds(HashSet<Guid> ids)
    {
        const string query = @"
            SELECT
                operation.BatchInvoices.Id,
                operation.BatchInvoices.InvoiceNumber,
                operation.BatchInvoices.VoucherDate,
                operation.BatchInvoices.ValueDate,
                operation.BatchInvoices.DueDate,
                operation.BatchInvoices.PaymentTerms,
                operation.BatchInvoices.PaymentType,
                operation.BatchInvoices.PaymentStatus,
                operation.BatchInvoices.CustomerName,
                operation.BatchInvoices.CustomerVatNumber,
                operation.BatchInvoices.CountryCode,
                operation.BatchInvoices.City,
                operation.BatchInvoices.StreetAddress,
                operation.BatchInvoices.PostalCode,
                operation.BatchInvoices.PostalArea,
                operation.BatchInvoices.ProcessBatchKey,
                operation.BatchInvoices.CreatedBy,
                operation.BatchInvoices.CreatedAt,
                operation.BatchInvoices.ModifiedBy,
                operation.BatchInvoices.ModifiedAt,
                operation.BatchInvoices.InvoiceTemplateName,
                operation.BatchInvoices.UserId,
                operation.BatchInvoices.UserCompanyId,
                operation.BatchInvoices.UserBankAccountId
            FROM
                operation.BatchInvoices
            WHERE
                operation.BatchInvoices.Id IN @Ids
        ";

        await using var db = new SqlConnection(AppSettings.DbDatabaseContext);
        var parameters = new { Ids = ids };
        return (await db.QueryAsync<BatchInvoice>(query, parameters)).ToList();
    }

    public async Task<List<BatchInvoiceItem>> GetBatchInvoiceItemsByIds(HashSet<Guid> ids)
    {
        const string query = @"
            SELECT
                operation.BatchInvoiceItems.Id,
                operation.BatchInvoiceItems.BatchInvoiceId,
                operation.BatchInvoiceItems.ItemText,
                operation.BatchInvoiceItems.ItemQuantity,
                operation.BatchInvoiceItems.ItemQuantityUnit,
                operation.BatchInvoiceItems.ItemAmount,
                operation.BatchInvoiceItems.ItemDiscountRate,
                operation.BatchInvoiceItems.ValueAmount,
                operation.BatchInvoiceItems.VatRate,
                operation.BatchInvoiceItems.GrossAmount,
                operation.BatchInvoiceItems.CurrencyCode
            FROM
                operation.BatchInvoiceItems
            WHERE
                operation.BatchInvoiceItems.Id IN @Ids
        ";

        await using var db = new SqlConnection(AppSettings.DbDatabaseContext);
        var parameters = new { Ids = ids };
        return (await db.QueryAsync<BatchInvoiceItem>(query, parameters)).ToList();
    }

    /// <inheritdoc/>
    public async Task<List<VatNumberPattern>> GetVatNumberPatterns()
    {
        return (await DbOperations.Retrieve<VatNumberPattern>()).ToList();
    }

    /// <inheritdoc/>
    public async Task<List<InvoiceTemplate>> GetInvoiceTemplates(bool isDeleted = false)
    {
        var filterBy = new { IsDeleted = isDeleted };
        return (await DbOperations.Retrieve<InvoiceTemplate>(filterBy: filterBy)).ToList();
    }

    /// <inheritdoc/>
    public async Task<List<InvoiceTemplate>> GetInvoiceTemplatesByNames(HashSet<string> names)
    {
        const string query = @"
            SELECT
                operation.InvoiceTemplates.Id,
                operation.InvoiceTemplates.Name,
                operation.InvoiceTemplates.Data,
                operation.InvoiceTemplates.ContentType,
                operation.InvoiceTemplates.ShortDescription,
                operation.InvoiceTemplates.GeneratedAt,
                operation.InvoiceTemplates.IsDeleted
            FROM
                operation.InvoiceTemplates
            WHERE
                operation.InvoiceTemplates.Name IN @Names
            AND
                operation.InvoiceTemplates.IsDeleted = 0
        ";

        await using var db = new SqlConnection(AppSettings.DbDatabaseContext);
        var parameters = new { Names = names };
        return (await db.QueryAsync<InvoiceTemplate>(query, parameters)).ToList();
    }

    /// <inheritdoc/>
    /// <exception cref="BusinessException">Throws an error code INVALID_TEMPLATE_ID.</exception>
    public async Task<InvoiceTemplate> GetInvoiceTemplate(Guid templateId, bool isDeleted = false)
    {
        var filterBy = new { Id = templateId, IsDeleted = isDeleted };
        var data = (await DbOperations.Retrieve<InvoiceTemplate>(filterBy: filterBy)).SingleOrDefault();
        return data ?? throw InvalidTemplateId;
    }

    /// <inheritdoc/>
    public async Task<Guid> CreateInvoiceTemplate(InvoiceTemplateDto template)
    {
        if (string.IsNullOrEmpty(template.InvoiceTemplateData.ContentType))
            throw InvalidContentType;

        var entity = new InvoiceTemplate
        {
            Id = Guid.NewGuid(),
            Name = template.TemplateName,
            Data = template.InvoiceTemplateData.ContentData,
            ContentType = template.InvoiceTemplateData.ContentType,
            ShortDescription = template.InvoiceTemplateDescription,
            GeneratedAt = _dateTimeService.Now,
            IsDeleted = false
        };

        await DbOperations.Insert(entity);
        return entity.Id;
    }

    /// <inheritdoc/>
    /// <exception cref="BusinessException">Throws an error code INVALID_TEMPLATE_ID.</exception>
    public async Task UpdateInvoiceTemplate(Guid templateId, InvoiceTemplateDataDto data)
    {
        if (string.IsNullOrEmpty(data.ContentType))
            throw InvalidContentType;

        var filterBy = new { Id = templateId, IsDeleted = false };
        var updateBy = new
        {
            Data =  data.ContentData,
            ContentType = data.ContentType,
            ShortDescription = data.Description
        };

        await DbOperations.Update<InvoiceTemplate>(updateBy, filterBy);
    }

    /// <inheritdoc/>
    public async Task RemoveInvoiceTemplate(Guid templateId)
    {
        var updateBy = new { IsDeleted = true };
        var filterBy = new { Id = templateId };

        await DbOperations.Update<InvoiceTemplate>(updateBy, filterBy);
    }

    /// <inheritdoc/>
    public async Task<BatchInvoiceProcessing?> GetBatchInvoiceProcessingByKey(Guid processBatchKey)
    {
        var filterBy = new { ProcessBatchKey = processBatchKey };
        return (await DbOperations.Retrieve<BatchInvoiceProcessing>(filterBy)).SingleOrDefault();
    }

    /// <inheritdoc/>
    public async Task<List<BatchInvoiceProcessing>> GetBatchInvoiceProcessingByStatus(ProcessingStatus status)
    {
        var filterBy = new { Status = status };
        return (await DbOperations.Retrieve<BatchInvoiceProcessing>(filterBy)).ToList();
    }

    /// <inheritdoc/>
    public async Task<Guid> CreateBatchInvoiceProcessing()
    {
        var entity = new BatchInvoiceProcessing
        {
            Id = Guid.NewGuid(),
            BatchProcessingTime = null,
            Status = ProcessingStatus.New,
            CreatedAt = _dateTimeService.Now
        };

        await DbOperations.Insert(entity);
        return entity.Id;
    }

    /// <inheritdoc/>
    public async Task UpdateBatchInvoiceProcessingById(BatchInvoiceProcessingDto data)
    {
        var filterBy = new { Id = data.ProcessingId };
        var updateBy = new
        {
            Status = data.ProcessingStatus,
            BatchProcessingTime = data.ProcessingTime
        };

        await DbOperations.Update<BatchInvoiceProcessing>(updateBy, filterBy);    
    }

    /// <inheritdoc/>
    public async Task CreateBatchInvoice(List<BatchInvoiceDto> data)
    {
        var entities = new List<BatchInvoice>();
        foreach (var dto in data)
        {
            var timestamp = _dateTimeService.Now;
            entities.Add(new BatchInvoice
            {
                Id = dto.Id ?? Guid.NewGuid(),
                InvoiceNumber = dto.InvoiceNumber,
                VoucherDate = dto.VoucherDate,
                ValueDate = dto.ValueDate,
                DueDate = dto.DueDate,
                PaymentTerms = dto.PaymentTerms,
                PaymentType = dto.PaymentType,
                PaymentStatus = dto.PaymentStatus,
                CustomerName = dto.CustomerName,
                CustomerVatNumber = dto.CustomerVatNumber,
                CountryCode = dto.CountryCode,
                City = dto.City,
                StreetAddress = dto.StreetAddress,
                PostalCode = dto.PostalCode,
                PostalArea = dto.PostalArea,
                InvoiceTemplateName = dto.InvoiceTemplateName,
                CreatedAt = timestamp,
                CreatedBy = dto.UserId,
                ModifiedAt = null,
                ModifiedBy = null,
                ProcessBatchKey = dto.ProcessBatchKey,
                UserId = dto.UserId,
                UserCompanyId = dto.UserCompanyId,
                UserBankAccountId = dto.UserBankAccountId
            });
        }

        await DbOperations.Insert(entities);
    }

    /// <inheritdoc/>
    public async Task CreateBatchInvoiceItem(List<BatchInvoiceItemDto> data)
    {
        var entities = new List<BatchInvoiceItem>();
        foreach (var dto in data)
        {
            entities.Add(new BatchInvoiceItem
            {
                Id = Guid.NewGuid(),
                BatchInvoiceId = dto.BatchInvoiceId,
                ItemText = dto.ItemText,
                ItemQuantity = dto.ItemQuantity,
                ItemQuantityUnit = dto.ItemQuantityUnit,
                ItemAmount = dto.ItemAmount,
                ItemDiscountRate = dto.ItemDiscountRate,
                ValueAmount = dto.ValueAmount,
                VatRate = dto.VatRate,
                GrossAmount = dto.GrossAmount,
                CurrencyCode = dto.CurrencyCode
            });
        }

        await DbOperations.Insert(entities);
    }

    /// <inheritdoc/>
    public async Task<InvoiceDataDto?> GetIssuedInvoiceById(string invoiceNumber)
    {
        var  filterBy = new { InvoiceNumber = invoiceNumber };
        var data = (await DbOperations.Retrieve<IssuedInvoice>(filterBy)).SingleOrDefault();
        if (data != null)
        {
            return new InvoiceDataDto
            {
                Number = data.InvoiceNumber,
                ContentData = data.InvoiceData,
                ContentType = data.ContentType,
                GeneratedAt = data.GeneratedAt
            };
        }

        return null;
    }

    /// <inheritdoc/>
    public async Task CreateIssuedInvoice(List<IssuedInvoiceDto> data)
    {
        var entities = new List<IssuedInvoice>();
        foreach (var dto in data)
        {
            entities.Add(new IssuedInvoice
            {
                Id = Guid.NewGuid(),
                UserId =  dto.UserId,
                InvoiceNumber = dto.InvoiceNumber,
                InvoiceData = dto.InvoiceData,
                ContentType = "text/html",
                GeneratedAt = _dateTimeService.Now
            });
        }

        await DbOperations.Insert(entities);
    }
}