﻿namespace TokanPages.WebApi.Controllers.Api;

using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Backend.Domain.Enums;
using Backend.Cqrs.Mappers;
using Backend.Dto.Subscribers;
using Backend.Shared.Attributes;
using Services.Caching.Subscribers;
using Backend.Cqrs.Handlers.Queries.Subscribers;
using MediatR;

/// <summary>
/// API endpoints definitions for subscribers
/// </summary>
[Authorize]
[ApiVersion("1.0")]
public class SubscribersController : ApiBaseController
{
    private readonly ISubscribersCache _subscribersCache;

    /// <summary>
    /// Subscribers controller
    /// </summary>
    /// <param name="mediator">Mediator instance</param>
    /// <param name="subscribersCache"></param>
    public SubscribersController(IMediator mediator, ISubscribersCache subscribersCache) 
        : base(mediator) => _subscribersCache = subscribersCache;

    /// <summary>
    /// Returns all registered subscribers
    /// </summary>
    /// <param name="noCache">Enable/disable REDIS cache</param>
    /// <returns>Object</returns>
    [HttpGet]
    [AuthorizeUser(Roles.GodOfAsgard)]
    [ProducesResponseType(typeof(IEnumerable<GetAllSubscribersQueryResult>), StatusCodes.Status200OK)]
    public async Task<IEnumerable<GetAllSubscribersQueryResult>> GetAllSubscribers([FromQuery] bool noCache = false)
        => await _subscribersCache.GetSubscribers(noCache);

    /// <summary>
    /// Returns registered subscriber
    /// </summary>
    /// <param name="id">Subscriber ID</param>
    /// <param name="noCache">Enable/disable REDIS cache</param>
    /// <returns>Object</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(GetSubscriberQueryResult), StatusCodes.Status200OK)]
    public async Task<GetSubscriberQueryResult> GetSubscriber([FromRoute] Guid id, [FromQuery] bool noCache = false)
        => await _subscribersCache.GetSubscriber(id, noCache);

    /// <summary>
    /// Adds new subscriber of the newsletter
    /// </summary>
    /// <param name="payLoad">Subscriber data</param>
    /// <returns>Guid</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    public async Task<Guid> AddSubscriber([FromBody] AddSubscriberDto payLoad) 
        => await Mediator.Send(SubscribersMapper.MapToAddSubscriberCommand(payLoad));

    /// <summary>
    /// Updates existing subscriber
    /// </summary>
    /// <param name="payLoad">Subscriber data</param>
    /// <returns>MediatR unit value</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> UpdateSubscriber([FromBody] UpdateSubscriberDto payLoad)
        => await Mediator.Send(SubscribersMapper.MapToUpdateSubscriberCommand(payLoad));

    /// <summary>
    /// Removes existing subscriber
    /// </summary>
    /// <param name="payLoad">Subscriber data</param>
    /// <returns>MediatR unit value</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> RemoveSubscriber([FromBody] RemoveSubscriberDto payLoad)
        => await Mediator.Send(SubscribersMapper.MapToRemoveSubscriberCommand(payLoad));
}