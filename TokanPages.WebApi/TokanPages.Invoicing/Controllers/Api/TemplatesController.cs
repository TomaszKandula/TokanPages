using Microsoft.AspNetCore.Mvc;
using MediatR;
using TokanPages.Backend.Application.Templates.Commands;
using TokanPages.Backend.Application.Templates.Queries;
using TokanPages.Backend.Application.Templates.Queries.Models;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Backend.Shared.Attributes;
using TokanPages.Invoicing.Controllers.Mappers;
using TokanPages.Invoicing.Dto.Templates;

namespace TokanPages.Invoicing.Controllers.Api;

/// <summary>
/// API endpoints definitions for invoice templates.
/// </summary>
///<remarks>
/// It uses Microsoft 'ResponseCache' for caching.
/// </remarks>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
public class TemplatesController : ApiBaseController
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="mediator"></param>
    public TemplatesController(IMediator mediator) : base(mediator) { }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [AuthorizeUser(Roles.GodOfAsgard, Roles.EverydayUser)]
    [ProducesResponseType(typeof(IEnumerable<InvoiceTemplateInfo>), StatusCodes.Status200OK)]
    public async Task<IEnumerable<InvoiceTemplateInfo>> GetInvoiceTemplates() 
        => await Mediator.Send(new GetInvoiceTemplatesQuery());

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:guid}")]
    [AuthorizeUser(Roles.GodOfAsgard, Roles.EverydayUser)]
    [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
    public async Task<FileContentResult> GetInvoiceTemplate([FromRoute] Guid id) 
        => await Mediator.Send(new GetInvoiceTemplateQuery { Id = id});

    /// <summary>
    /// 
    /// </summary>
    /// <param name="payload"></param>
    /// <returns></returns>
    [HttpPost]
    [AuthorizeUser(Roles.GodOfAsgard, Roles.EverydayUser)]
    [ProducesResponseType(typeof(AddInvoiceTemplateCommandResult), StatusCodes.Status200OK)]
    public async Task<AddInvoiceTemplateCommandResult> AddInvoiceTemplate([FromForm] AddInvoiceTemplateDto payload)
        => await Mediator.Send(TemplatesMapper.MapToAddInvoiceTemplateCommandRequest(payload));

    /// <summary>
    /// 
    /// </summary>
    /// <param name="payload"></param>
    /// <returns></returns>
    [HttpPost]
    [AuthorizeUser(Roles.GodOfAsgard, Roles.EverydayUser)]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> ReplaceInvoiceTemplate([FromForm] ReplaceInvoiceTemplateDto payload) 
        => await Mediator.Send(TemplatesMapper.MapToReplaceInvoiceTemplateCommandRequest(payload));

    /// <summary>
    /// 
    /// </summary>
    /// <param name="payload"></param>
    /// <returns></returns>
    [HttpPost]
    [AuthorizeUser(Roles.GodOfAsgard, Roles.EverydayUser)]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> RemoveInvoiceTemplate([FromBody] RemoveInvoiceTemplateDto payload) 
        => await Mediator.Send(TemplatesMapper.MapToRemoveInvoiceTemplateCommandRequest(payload));
}