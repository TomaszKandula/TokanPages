using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Backend.Shared.Attributes;
using MediatR;
using TokanPages.Backend.Application.Subscribers.Queries;
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
    private readonly ISubscribersCache _subscribersCache;

    /// <summary>
    /// Subscribers controller.
    /// </summary>
    /// <param name="mediator">Mediator instance.</param>
    /// <param name="subscribersCache">REDIS cache instance.</param>
    public NewslettersController(IMediator mediator, ISubscribersCache subscribersCache) 
        : base(mediator) => _subscribersCache = subscribersCache;

    /// <summary>
    /// Returns all registered subscribers.
    /// </summary>
    /// <param name="noCache">Enable/disable REDIS cache.</param>
    /// <returns>Object</returns>
    [HttpGet]
    [Route("[action]")]
    [AuthorizeUser(Roles.GodOfAsgard)]
    [ProducesResponseType(typeof(IEnumerable<GetAllNewslettersQueryResult>), StatusCodes.Status200OK)]
    public async Task<IEnumerable<GetAllNewslettersQueryResult>> GetAllSubscribers([FromQuery] bool noCache = false)
        => await _subscribersCache.GetSubscribers(noCache);

    /// <summary>
    /// Returns registered subscriber.
    /// </summary>
    /// <param name="id">Subscriber ID.</param>
    /// <param name="noCache">Enable/disable REDIS cache.</param>
    /// <returns>Object.</returns>
    [HttpGet]
    [Route("{id:guid}/[action]")]
    [ProducesResponseType(typeof(GetNewsletterQueryResult), StatusCodes.Status200OK)]
    public async Task<GetNewsletterQueryResult> GetSubscriber([FromRoute] Guid id, [FromQuery] bool noCache = false)
        => await _subscribersCache.GetSubscriber(id, noCache);

    /// <summary>
    /// Adds new subscriber of the newsletter.
    /// </summary>
    /// <param name="payLoad">Subscriber data.</param>
    /// <returns>Guid.</returns>
    [HttpPost]
    [Route("[action]")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    public async Task<Guid> AddSubscriber([FromBody] AddNewsletterDto payLoad) 
        => await Mediator.Send(NewslettersMapper.MapToAddSubscriberCommand(payLoad));

    /// <summary>
    /// Updates existing subscriber.
    /// </summary>
    /// <param name="payLoad">Subscriber data.</param>
    /// <returns>MediatR unit value.</returns>
    [HttpPost]
    [Route("[action]")]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> UpdateSubscriber([FromBody] UpdateNewsletterDto payLoad)
        => await Mediator.Send(NewslettersMapper.MapToUpdateSubscriberCommand(payLoad));

    /// <summary>
    /// Removes existing subscriber.
    /// </summary>
    /// <param name="payLoad">Subscriber data.</param>
    /// <returns>MediatR unit value.</returns>
    [HttpPost]
    [Route("[action]")]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> RemoveSubscriber([FromBody] RemoveNewsletterDto payLoad)
        => await Mediator.Send(NewslettersMapper.MapToRemoveSubscriberCommand(payLoad));
}