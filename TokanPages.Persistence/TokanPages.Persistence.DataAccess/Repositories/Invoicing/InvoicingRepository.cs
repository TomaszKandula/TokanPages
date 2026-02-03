using Microsoft.Extensions.Options;
using TokanPages.Backend.Configuration.Options;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Domain.Entities.Invoicing;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.DataAccess.Abstractions;
using TokanPages.Persistence.DataAccess.Repositories.Invoicing.Models;

namespace TokanPages.Persistence.DataAccess.Repositories.Invoicing;

public class InvoicingRepository : RepositoryBase, IInvoicingRepository
{
    public InvoicingRepository(IDbOperations dbOperations, IOptions<AppSettingsModel> appSettings) 
        : base(dbOperations, appSettings) { }

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
}