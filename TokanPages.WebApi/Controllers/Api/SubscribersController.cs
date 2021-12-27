namespace TokanPages.WebApi.Controllers.Api;

using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Attributes;
using Backend.Cqrs.Mappers;
using Backend.Identity.Authorization;
using Backend.Shared.Dto.Subscribers;
using Backend.Cqrs.Handlers.Queries.Subscribers;
using Services.Caching.Subscribers;
using MediatR;

[Authorize]
[ApiVersion("1.0")]
public class SubscribersController : ApiBaseController
{
    private readonly ISubscribersCache _subscribersCache;

    public SubscribersController(IMediator mediator, ISubscribersCache subscribersCache) 
        : base(mediator) => _subscribersCache = subscribersCache;

    [HttpGet]
    [AuthorizeUser(Roles.GodOfAsgard)]
    [ProducesResponseType(typeof(IEnumerable<GetAllSubscribersQueryResult>), StatusCodes.Status200OK)]
    public async Task<IEnumerable<GetAllSubscribersQueryResult>> GetAllSubscribers([FromQuery] bool noCache = false)
        => await _subscribersCache.GetSubscribers(noCache);

    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(GetSubscriberQueryResult), StatusCodes.Status200OK)]
    public async Task<GetSubscriberQueryResult> GetSubscriber([FromRoute] Guid id, [FromQuery] bool noCache = false)
        => await _subscribersCache.GetSubscriber(id, noCache);

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    public async Task<Guid> AddSubscriber([FromBody] AddSubscriberDto payLoad) 
        => await Mediator.Send(SubscribersMapper.MapToAddSubscriberCommand(payLoad));

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> UpdateSubscriber([FromBody] UpdateSubscriberDto payLoad)
        => await Mediator.Send(SubscribersMapper.MapToUpdateSubscriberCommand(payLoad));

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> RemoveSubscriber([FromBody] RemoveSubscriberDto payLoad)
        => await Mediator.Send(SubscribersMapper.MapToRemoveSubscriberCommand(payLoad));
}