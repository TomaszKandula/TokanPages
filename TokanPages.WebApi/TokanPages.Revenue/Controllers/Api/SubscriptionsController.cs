using TokanPages.Backend.Application.Revenue.Commands;
using TokanPages.Backend.Application.Revenue.Queries;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Backend.Shared.Attributes;
using TokanPages.Revenue.Controllers.Mappers;
using TokanPages.Persistence.Caching.Abstractions;
using TokanPages.Revenue.Dto.Subscriptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TokanPages.Revenue.Controllers.Api;

/// <summary>
/// API endpoints definitions for user subscriptions.
/// </summary>
[Authorize]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
public class SubscriptionsController : ApiBaseController
{
    private readonly ISubscriptionsCache _subscriptionsCache;

    /// <inheritdoc />
    public SubscriptionsController(IMediator mediator, ISubscriptionsCache subscriptionsCache) 
        : base(mediator) => _subscriptionsCache = subscriptionsCache;

    /// <summary>
    /// Returns subscription price list.
    /// </summary>
    /// <param name="languageIso">Obligatory language ISO (three letter code).</param>
    /// <param name="noCache">Enable/Disable REDIS cache.</param>
    /// <returns>Price list.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(GetSubscriptionPricesQueryResult), StatusCodes.Status200OK)]
    public async Task<GetSubscriptionPricesQueryResult> GetSubscriptionPrice(
        [FromQuery] string languageIso, 
        [FromQuery] bool noCache = false)
        => await _subscriptionsCache.GetSubscriptionPrices(languageIso, noCache);

    /// <summary>
    /// Returns subscription details for a currently logged user, or by given user ID.
    /// </summary>
    /// <param name="userId">User ID.</param>
    /// <returns>Subscription details.</returns>
    [HttpGet]
    [AuthorizeUser(Roles.EverydayUser)]
    [ProducesResponseType(typeof(GetSubscriptionQueryResult), StatusCodes.Status200OK)]
    public async Task<GetSubscriptionQueryResult> GetSubscription([FromQuery] Guid? userId)
        => await Mediator.Send(new GetSubscriptionQuery { UserId = userId });

    /// <summary>
    /// Adds user subscription with given payment terms.
    /// </summary>
    /// <param name="payload">User ID and selected payment term.</param>
    /// <returns>Subscription details.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(AddSubscriptionCommandResult),StatusCodes.Status200OK)]
    public async Task<AddSubscriptionCommandResult> AddSubscription([FromBody] AddSubscriptionDto payload) 
        => await Mediator.Send(SubscriptionsMapper.MapToAddSubscriptionCommand(payload));

    /// <summary>
    /// Updates existing user subscription.
    /// </summary>
    /// <param name="payload">Optional properties for modification.</param>
    /// <returns>MediatR unit value.</returns>
    [HttpPost]
    [AuthorizeUser(Roles.EverydayUser)]
    [ProducesResponseType(typeof(Unit),StatusCodes.Status200OK)]
    public async Task<Unit> UpdateSubscription([FromBody] UpdateSubscriptionDto payload)
        => await Mediator.Send(SubscriptionsMapper.MapToUpdateSubscriptionCommand(payload));

    /// <summary>
    /// Removes user subscription by its ID.
    /// </summary>
    /// <remarks>
    /// Requires: Roles.OrdinaryUser
    /// </remarks>
    /// <param name="payload">Optional user ID and mandatory subscription ID.</param>
    /// <returns>MediatR unit value.</returns>
    [HttpDelete]
    [AuthorizeUser(Roles.EverydayUser)]
    [ProducesResponseType(typeof(Unit),StatusCodes.Status200OK)]
    public async Task<Unit> RemoveSubscription([FromBody] RemoveSubscriptionDto payload)
        => await Mediator.Send(SubscriptionsMapper.MapToRemoveSubscriptionCommand(payload));
}