using Microsoft.Extensions.Options;
using TokanPages.Backend.Configuration.Options;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Domain.Entities.Invoicing;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Backend.Shared.Resources;
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
    /// <exception cref="BusinessException">Throws an error code INVALID_TEMPLATE_ID.</exception>
    public async Task<InvoiceTemplate> GetInvoiceTemplate(Guid templateId, bool isDeleted = false)
    {
        var filterBy = new { Id = templateId, IsDeleted = isDeleted };
        var data = (await DbOperations.Retrieve<InvoiceTemplate>(filterBy: filterBy)).SingleOrDefault();
        return data ?? throw InvalidTemplateId;
    }

    /// <inheritdoc/>
    public async Task<Guid> CreateInvoiceTemplate(InvoiceTemplateDto template, DateTime generatedAt)
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
            GeneratedAt = generatedAt,
            IsDeleted = false
        };

        await DbOperations.Insert(entity);
        return entity.Id;
    }

    /// <inheritdoc/>
    /// <exception cref="BusinessException">Throws an error code INVALID_TEMPLATE_ID.</exception>
    public async Task<bool> ReplaceInvoiceTemplate(Guid templateId, InvoiceTemplateDataDto data)
    {
        try
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
        catch
        {
            return false;
        }

        return true;
    }

    /// <inheritdoc/>
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
    public async Task<bool> UpdateBatchInvoiceProcessingById(BatchInvoiceProcessingDto data)
    {
        try
        {
            var filterBy = new { Id = data.ProcessingId };
            var updateBy = new
            {
                Status = data.ProcessingStatus,
                BatchProcessingTime = data.ProcessingTime
            };

            await DbOperations.Update<BatchInvoiceProcessing>(updateBy, filterBy);    
        }
        catch
        {
            return false;
        }

        return true;        
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
    public async Task<Guid> CreateIssuedInvoice(Guid userId, string invoiceNumber, byte[] invoiceData)
    {
        var entity = new IssuedInvoice
        {
            Id = Guid.NewGuid(),
            UserId =  userId,
            InvoiceNumber = invoiceNumber,
            InvoiceData = invoiceData,
            ContentType = "text/html",
            GeneratedAt = _dateTimeService.Now
        };

        await DbOperations.Insert(entity);
        return entity.Id;
    }

    /// <inheritdoc/>
    public async Task<Guid> CreateBatchInvoiceProcessing(DateTime createdAt)
    {
        var entity = new BatchInvoiceProcessing
        {
            Id = Guid.NewGuid(),
            BatchProcessingTime = null,
            Status = ProcessingStatus.New,
            CreatedAt = createdAt
        };

        await DbOperations.Insert(entity);
        return entity.Id;
    }

    /// <inheritdoc/>
    public async Task<Guid> CreateBatchInvoice(BatchInvoiceDto data)
    {
        var entity = new BatchInvoice
        {
            Id = Guid.NewGuid(),
            InvoiceNumber = data.InvoiceNumber,
            VoucherDate = data.VoucherDate,
            ValueDate = data.ValueDate,
            DueDate = data.DueDate,
            PaymentTerms = data.PaymentTerms,
            PaymentType = data.PaymentType,
            PaymentStatus = data.PaymentStatus,
            CustomerName = data.CustomerName,
            CustomerVatNumber = data.CustomerVatNumber,
            CountryCode = data.CountryCode,
            City = data.City,
            StreetAddress = data.StreetAddress,
            PostalCode = data.PostalCode,
            PostalArea = data.PostalArea,
            InvoiceTemplateName = data.InvoiceTemplateName,
            CreatedAt = data.CreatedAt,
            CreatedBy = data.UserId,
            ModifiedAt = null,
            ModifiedBy = null,
            ProcessBatchKey = data.ProcessBatchKey,
            UserId = data.UserId,
            UserCompanyId = data.UserCompanyId,
            UserBankAccountId = data.UserBankAccountId
        };

        await DbOperations.Insert(entity);
        return entity.Id;
    }

    public async Task<Guid> CreateBatchInvoiceItem(BatchInvoiceItemDto data)
    {
        var entity = new BatchInvoiceItem
        {
            Id = Guid.NewGuid(),
            BatchInvoiceId = data.BatchInvoiceId,
            ItemText = data.ItemText,
            ItemQuantity = data.ItemQuantity,
            ItemQuantityUnit = data.ItemQuantityUnit,
            ItemAmount = data.ItemAmount,
            ItemDiscountRate = data.ItemDiscountRate,
            ValueAmount = data.ValueAmount,
            VatRate = data.VatRate,
            GrossAmount = data.GrossAmount,
            CurrencyCode = data.CurrencyCode
        };

        await  DbOperations.Insert(entity);
        return entity.Id;
    }
}