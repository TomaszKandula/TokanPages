using Microsoft.EntityFrameworkCore;
using TokanPages.Services.TemplateService.Models;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Domain.Entities.Invoicing;
using TokanPages.Persistence.Database;

namespace TokanPages.Services.TemplateService;

public class TemplateService : ITemplateService
{
    private readonly DatabaseContext _databaseContext;

    private readonly IDateTimeService _dateTimeService;

    public TemplateService(DatabaseContext databaseContext, IDateTimeService dateTimeService)
    {
        _databaseContext = databaseContext;
        _dateTimeService = dateTimeService;
    }

    /// <summary>
    /// Returns list of registered invoices templates within the system.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns></returns>
    public async Task<IEnumerable<InvoiceTemplateInfo>> GetInvoiceTemplates(CancellationToken cancellationToken = default)
        => await _databaseContext.InvoiceTemplates
            .Where(templates => !templates.IsDeleted)
            .Select(templates => new InvoiceTemplateInfo
            {
                Id = templates.Id,
                Name = templates.Name
            })
            .ToListAsync(cancellationToken);

    /// <summary>
    /// Returns registered invoice template by ID.
    /// </summary>
    /// <param name="templateId">Invoice template ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Template file data.</returns>
    /// <exception cref="BusinessException">Throws an error code INVALID_TEMPLATE_ID.</exception>
    public async Task<InvoiceTemplateData> GetInvoiceTemplate(Guid templateId, CancellationToken cancellationToken = default)
    {
        var template = await _databaseContext.InvoiceTemplates
            .AsNoTracking()
            .Where(templates => templates.Id == templateId)
            .Where(templates => !templates.IsDeleted)
            .Select(templates => new InvoiceTemplateData
            {
                ContentData = templates.Data,
                ContentType = templates.ContentType
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (template == null)
            throw new BusinessException(nameof(ErrorCodes.INVALID_TEMPLATE_ID), ErrorCodes.INVALID_TEMPLATE_ID);

        return template;
    }

    /// <summary>
    /// Remove (soft delete) existing invoice template.
    /// </summary>
    /// <param name="templateId">Invoice template ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <exception cref="BusinessException">Throws an error code INVALID_TEMPLATE_ID.</exception>
    public async Task RemoveInvoiceTemplate(Guid templateId, CancellationToken cancellationToken = default)
    {
        var template = await _databaseContext.InvoiceTemplates
            .Where(templates => templates.Id == templateId)
            .Where(templates => !templates.IsDeleted)
            .FirstOrDefaultAsync(cancellationToken);

        if (template == null)
            throw new BusinessException(nameof(ErrorCodes.INVALID_TEMPLATE_ID), ErrorCodes.INVALID_TEMPLATE_ID);

        template.IsDeleted = true;
        await _databaseContext.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Updates current invoice template.
    /// </summary>
    /// <param name="templateId">Invoice template ID.</param>
    /// <param name="invoiceTemplateData">Holds Binary representation of a new invoice template and its content type.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <exception cref="BusinessException">Throws an error code INVALID_TEMPLATE_ID.</exception>
    public async Task ReplaceInvoiceTemplate(Guid templateId, InvoiceTemplateData invoiceTemplateData, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(invoiceTemplateData.ContentType))
            throw new BusinessException(nameof(ErrorCodes.INVALID_CONTENT_TYPE), ErrorCodes.INVALID_CONTENT_TYPE);

        var template = await _databaseContext.InvoiceTemplates
            .Where(templates => templates.Id == templateId)
            .Where(templates => !templates.IsDeleted)
            .FirstOrDefaultAsync(cancellationToken);

        if (template == null)
            throw new BusinessException(nameof(ErrorCodes.INVALID_TEMPLATE_ID), ErrorCodes.INVALID_TEMPLATE_ID);

        template.Data = invoiceTemplateData.ContentData;
        template.ContentType = invoiceTemplateData.ContentType;
        template.ShortDescription = invoiceTemplateData.Description;
        await _databaseContext.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Adds new invoice template to the database.
    /// </summary>
    /// <param name="invoiceTemplate">Invoice template data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Invoice template ID.</returns>
    public async Task<Guid> AddInvoiceTemplate(InvoiceTemplate invoiceTemplate, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(invoiceTemplate.InvoiceTemplateData.ContentType))
            throw new BusinessException(nameof(ErrorCodes.INVALID_CONTENT_TYPE), ErrorCodes.INVALID_CONTENT_TYPE);

        var template = new InvoiceTemplates
        {
            Name = invoiceTemplate.TemplateName,
            Data = invoiceTemplate.InvoiceTemplateData.ContentData,
            ContentType = invoiceTemplate.InvoiceTemplateData.ContentType,
            ShortDescription = invoiceTemplate.InvoiceTemplateDescription,
            GeneratedAt = _dateTimeService.Now,
            IsDeleted = false
        };

        await _databaseContext.AddAsync(template, cancellationToken);
        await _databaseContext.SaveChangesAsync(cancellationToken);

        return template.Id;
    }
}