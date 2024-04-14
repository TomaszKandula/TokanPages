﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Backend.Shared.Attributes;
using MediatR;
using TokanPages.Backend.Application.Newsletters.Queries;
using TokanPages.Persistence.Caching.Abstractions;
using TokanPages.Sender.Controllers.Mappers;
using TokanPages.Sender.Dto.Newsletters;

namespace TokanPages.Sender.Controllers.Api;

/// <summary>
/// API endpoints definitions for subscribers.
/// </summary>
[Authorize]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class NewslettersController : ApiBaseController
{
    private readonly INewslettersCache _newslettersCache;

    /// <summary>
    /// Subscribers controller.
    /// </summary>
    /// <param name="mediator">Mediator instance.</param>
    /// <param name="newslettersCache">REDIS cache instance.</param>
    public NewslettersController(IMediator mediator, INewslettersCache newslettersCache) 
        : base(mediator) => _newslettersCache = newslettersCache;

    /// <summary>
    /// Returns all registered subscribers.
    /// </summary>
    /// <param name="noCache">Enable/disable REDIS cache.</param>
    /// <returns>Object</returns>
    [HttpGet]
    [Route("[action]")]
    [AuthorizeUser(Roles.GodOfAsgard)]
    [ProducesResponseType(typeof(IEnumerable<GetAllNewslettersQueryResult>), StatusCodes.Status200OK)]
    public async Task<IEnumerable<GetAllNewslettersQueryResult>> GetAllNewsletters([FromQuery] bool noCache = false)
        => await _newslettersCache.GetNewsletters(noCache);

    /// <summary>
    /// Returns registered newsletters.
    /// </summary>
    /// <param name="id">Newsletters ID.</param>
    /// <param name="noCache">Enable/disable REDIS cache.</param>
    /// <returns>Object.</returns>
    [HttpGet]
    [Route("{id:guid}/[action]")]
    [ProducesResponseType(typeof(GetNewsletterQueryResult), StatusCodes.Status200OK)]
    public async Task<GetNewsletterQueryResult> GetNewsletter([FromRoute] Guid id, [FromQuery] bool noCache = false)
        => await _newslettersCache.GetNewsletter(id, noCache);

    /// <summary>
    /// Adds new subscriber of the newsletter.
    /// </summary>
    /// <param name="payLoad">Newsletters data.</param>
    /// <returns>Guid.</returns>
    [HttpPost]
    [Route("[action]")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    public async Task<Guid> AddNewsletter([FromBody] AddNewsletterDto payLoad) 
        => await Mediator.Send(NewslettersMapper.MapToAddNewsletterCommand(payLoad));

    /// <summary>
    /// Updates existing newsletter subscriber.
    /// </summary>
    /// <param name="payLoad">Newsletters data.</param>
    /// <returns>MediatR unit value.</returns>
    [HttpPost]
    [Route("[action]")]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> UpdateNewsletter([FromBody] UpdateNewsletterDto payLoad)
        => await Mediator.Send(NewslettersMapper.MapToUpdateNewsletterCommand(payLoad));

    /// <summary>
    /// Removes existing newsletter subscriber.
    /// </summary>
    /// <param name="payLoad">Newsletter data.</param>
    /// <returns>MediatR unit value.</returns>
    [HttpPost]
    [Route("[action]")]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> RemoveNewsletter([FromBody] RemoveNewsletterDto payLoad)
        => await Mediator.Send(NewslettersMapper.MapToRemoveNewsletterCommand(payLoad));
}