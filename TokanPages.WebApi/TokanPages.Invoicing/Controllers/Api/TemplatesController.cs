using Microsoft.AspNetCore.Mvc;
using MediatR;
using TokanPages.Backend.Application.Templates.Commands;
using TokanPages.Backend.Application.Templates.Queries;
using TokanPages.Backend.Application.Templates.Queries.Models;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Backend.Shared.Attributes;
using TokanPages.Invoicing.Controllers.Mappers;
using TokanPages.Invoicing.Dto.Templates;
using TokanPages.Persistence.Caching.Abstractions;

namespace TokanPages.Invoicing.Controllers.Api;

/// <summary>
/// API endpoints definitions for invoice templates.
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
public class TemplatesController : ApiBaseController
{
    private readonly ITemplatesCache _templatesCache;

    /// <summary>
    /// Templates Controller.
    /// </summary>
    /// <param name="mediator">Mediator instance.</param>
    /// <param name="templatesCache">REDIS cache instance.</param>
    public TemplatesController(IMediator mediator, ITemplatesCache templatesCache) 
        : base(mediator) => _templatesCache = templatesCache;

    /// <summary>
    /// Returns information of existing invoice templates.
    /// </summary>
    /// <param name="noCache">Enable/disable REDIS cache.</param>
    /// <returns>List of templates.</returns>
    [HttpGet]
    [AuthorizeUser(Roles.GodOfAsgard, Roles.EverydayUser)]
    [ProducesResponseType(typeof(IList<InvoiceTemplateInfo>), StatusCodes.Status200OK)]
    public async Task<IList<InvoiceTemplateInfo>> GetInvoiceTemplates([FromQuery] bool noCache = false) 
        => await _templatesCache.GetInvoiceTemplates(noCache);

    /// <summary>
    /// Returns invoice template by its ID.
    /// </summary>
    /// <param name="id">Template ID.</param>
    /// <returns>File.</returns>
    [HttpGet("{id:guid}")]
    [AuthorizeUser(Roles.GodOfAsgard, Roles.EverydayUser)]
    [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
    public async Task<FileContentResult> GetInvoiceTemplate([FromRoute] Guid id) 
        => await Mediator.Send(new GetInvoiceTemplateQuery { Id = id});

    /// <summary>
    /// Allows to add new invoice template.
    /// </summary>
    /// <param name="payload">Invoice template.</param>
    /// <returns>Assigned template ID.</returns>
    [HttpPost]
    [AuthorizeUser(Roles.GodOfAsgard, Roles.EverydayUser)]
    [ProducesResponseType(typeof(AddInvoiceTemplateCommandResult), StatusCodes.Status200OK)]
    public async Task<AddInvoiceTemplateCommandResult> AddInvoiceTemplate([FromForm] AddInvoiceTemplateDto payload)
        => await Mediator.Send(TemplatesMapper.MapToAddInvoiceTemplateCommandRequest(payload));

    /// <summary>
    /// Allows to update existing invoice template.  
    /// </summary>
    /// <param name="payload">Invoice template.</param>
    /// <returns>Empty object.</returns>
    [HttpPost]
    [AuthorizeUser(Roles.GodOfAsgard, Roles.EverydayUser)]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> ReplaceInvoiceTemplate([FromForm] ReplaceInvoiceTemplateDto payload) 
        => await Mediator.Send(TemplatesMapper.MapToReplaceInvoiceTemplateCommandRequest(payload));

    /// <summary>
    /// Allows to remove exising invoice template.
    /// </summary>
    /// <param name="payload">Template ID.</param>
    /// <returns>Empty object.</returns>
    [HttpPost]
    [AuthorizeUser(Roles.GodOfAsgard, Roles.EverydayUser)]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> RemoveInvoiceTemplate([FromBody] RemoveInvoiceTemplateDto payload) 
        => await Mediator.Send(TemplatesMapper.MapToRemoveInvoiceTemplateCommandRequest(payload));
}