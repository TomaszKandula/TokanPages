using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TokanPages.WebApi.Controllers.Mappers;
using TokanPages.WebApi.Dto.NotificationsWeb;

namespace TokanPages.WebApi.Controllers.Api;

/// <summary>
/// API endpoints definitions for websocket notifications.
/// </summary>
[Authorize]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
public class NotificationsWebController : ApiBaseController
{
    /// <inheritdoc />
    public NotificationsWebController(IMediator mediator) : base(mediator) { }

    /// <summary>
    /// Notifies front-end via web socket connection.
    /// </summary>
    /// <param name="payload">Optional payload and notification method.</param>
    /// <returns>MediatR unit value.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Unit),StatusCodes.Status200OK)]
    public async Task<Unit> Notify([FromBody] NotifyRequestDto payload) 
        => await Mediator.Send(NotificationsWebMapper.MapToNotifyRequestCommand(payload));
}