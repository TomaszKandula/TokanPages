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

        await using var connection = new SqlConnection(AppSettings.DbDatabaseContext);
        var parameters = new { UserIds = userIds };
        var result = await connection.QueryAsync<UserCompany>(query, parameters);
        return result.ToList();
    }

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

        await using var connection = new SqlConnection(AppSettings.DbDatabaseContext);
        var parameters = new { UserIds = userIds };
        var result = await connection.QueryAsync<UserBankAccount>(query, parameters);
        return result.ToList();
    }

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

        await using var connection = new SqlConnection(AppSettings.DbDatabaseContext);
        var parameters = new { Ids = ids };
        var result = await connection.QueryAsync<BatchInvoice>(query, parameters);
        return result.ToList();
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

        await using var connection = new SqlConnection(AppSettings.DbDatabaseContext);
        var parameters = new { Ids = ids };
        var result = await connection.QueryAsync<BatchInvoiceItem>(query, parameters);
        return result.ToList();
    }

    public async Task<List<VatNumberPattern>> GetVatNumberPatterns()
    {
        var result = await DbOperations.Retrieve<VatNumberPattern>();
        return result.ToList();
    }

    public async Task<List<InvoiceTemplate>> GetInvoiceTemplates(bool isDeleted = false)
    {
        var filterBy = new { IsDeleted = isDeleted };
        var result = await DbOperations.Retrieve<InvoiceTemplate>(filterBy);
        return result.ToList();
    }

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

        await using var connection = new SqlConnection(AppSettings.DbDatabaseContext);
        var parameters = new { Names = names };
        return (await connection.QueryAsync<InvoiceTemplate>(query, parameters)).ToList();
    }

    public async Task<InvoiceTemplate> GetInvoiceTemplate(Guid templateId, bool isDeleted = false)
    {
        var filterBy = new { Id = templateId, IsDeleted = isDeleted };
        var data = (await DbOperations.Retrieve<InvoiceTemplate>(filterBy: filterBy)).SingleOrDefault();
        return data ?? throw InvalidTemplateId;
    }

    public async Task<Guid> CreateInvoiceTemplate(InvoiceTemplateDto data)
    {
        if (string.IsNullOrEmpty(data.InvoiceTemplateData.ContentType))
            throw InvalidContentType;

        var entity = new InvoiceTemplate
        {
            Id = Guid.NewGuid(),
            Name = data.TemplateName,
            Data = data.InvoiceTemplateData.ContentData,
            ContentType = data.InvoiceTemplateData.ContentType,
            ShortDescription = data.InvoiceTemplateDescription,
            GeneratedAt = _dateTimeService.Now,
            IsDeleted = false
        };

        await DbOperations.Insert(entity);
        return entity.Id;
    }

    public async Task UpdateInvoiceTemplate(Guid templateId, InvoiceTemplateDataDto data)
    {
        if (string.IsNullOrEmpty(data.ContentType))
            throw InvalidContentType;

        var updateBy = new
        {
            Data =  data.ContentData,
            ContentType = data.ContentType,
            ShortDescription = data.Description
        };

        var filterBy = new
        {
            Id = templateId, 
            IsDeleted = false
        };

        await DbOperations.Update<InvoiceTemplate>(updateBy, filterBy);
    }

    public async Task RemoveInvoiceTemplate(Guid templateId)
    {
        var updateBy = new { IsDeleted = true };
        var filterBy = new { Id = templateId };

        await DbOperations.Update<InvoiceTemplate>(updateBy, filterBy);
    }

    public async Task<BatchInvoiceProcessing?> GetBatchInvoiceProcessingByKey(Guid processBatchKey)
    {
        var filterBy = new { ProcessBatchKey = processBatchKey };
        var result = await DbOperations.Retrieve<BatchInvoiceProcessing>(filterBy);
        return result.SingleOrDefault();
    }

    public async Task<List<BatchInvoiceProcessing>> GetBatchInvoiceProcessingByStatus(ProcessingStatus status)
    {
        var filterBy = new { Status = status };
        var result = await DbOperations.Retrieve<BatchInvoiceProcessing>(filterBy);
        return result.ToList();
    }

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

    public async Task CreateBatchInvoice(List<BatchInvoiceDto> data)
    {
        var entities = new List<BatchInvoice>();
        foreach (var item in data)
        {
            var timestamp = _dateTimeService.Now;
            entities.Add(new BatchInvoice
            {
                Id = item.Id ?? Guid.NewGuid(),
                InvoiceNumber = item.InvoiceNumber,
                VoucherDate = item.VoucherDate,
                ValueDate = item.ValueDate,
                DueDate = item.DueDate,
                PaymentTerms = item.PaymentTerms,
                PaymentType = item.PaymentType,
                PaymentStatus = item.PaymentStatus,
                CustomerName = item.CustomerName,
                CustomerVatNumber = item.CustomerVatNumber,
                CountryCode = item.CountryCode,
                City = item.City,
                StreetAddress = item.StreetAddress,
                PostalCode = item.PostalCode,
                PostalArea = item.PostalArea,
                InvoiceTemplateName = item.InvoiceTemplateName,
                CreatedAt = timestamp,
                CreatedBy = item.UserId,
                ModifiedAt = null,
                ModifiedBy = null,
                ProcessBatchKey = item.ProcessBatchKey,
                UserId = item.UserId,
                UserCompanyId = item.UserCompanyId,
                UserBankAccountId = item.UserBankAccountId
            });
        }

        await DbOperations.Insert(entities);
    }

    public async Task CreateBatchInvoiceItem(List<BatchInvoiceItemDto> data)
    {
        var entities = new List<BatchInvoiceItem>();
        foreach (var item in data)
        {
            entities.Add(new BatchInvoiceItem
            {
                Id = Guid.NewGuid(),
                BatchInvoiceId = item.BatchInvoiceId,
                ItemText = item.ItemText,
                ItemQuantity = item.ItemQuantity,
                ItemQuantityUnit = item.ItemQuantityUnit,
                ItemAmount = item.ItemAmount,
                ItemDiscountRate = item.ItemDiscountRate,
                ValueAmount = item.ValueAmount,
                VatRate = item.VatRate,
                GrossAmount = item.GrossAmount,
                CurrencyCode = item.CurrencyCode
            });
        }

        await DbOperations.Insert(entities);
    }

    public async Task<InvoiceDataDto?> GetIssuedInvoiceById(string invoiceNumber)
    {
        var  filterBy = new { InvoiceNumber = invoiceNumber };
        var results = await DbOperations.Retrieve<IssuedInvoice>(filterBy);

        var data = results.SingleOrDefault();
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

    public async Task CreateIssuedInvoice(List<IssuedInvoiceDto> data)
    {
        var entities = new List<IssuedInvoice>();
        foreach (var item in data)
        {
            entities.Add(new IssuedInvoice
            {
                Id = Guid.NewGuid(),
                UserId =  item.UserId,
                InvoiceNumber = item.InvoiceNumber,
                InvoiceData = item.InvoiceData,
                ContentType = "text/html",
                GeneratedAt = _dateTimeService.Now
            });
        }

        await DbOperations.Insert(entities);
    }
}