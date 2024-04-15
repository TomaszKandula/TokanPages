using TokanPages.Backend.Application.Notifications.Web.Command;
using TokanPages.Notifications.Controllers.Mappers;
using TokanPages.Notifications.Dto.NotificationsWeb;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TokanPages.Notifications.Controllers.Api;

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
    /// <param name="payload">Data payload including user ID, optional payload and notification method.</param>
    /// <returns>Status ID to check notification if WebSocket files.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(NotifyRequestCommandResult),StatusCodes.Status200OK)]
    public async Task<NotifyRequestCommandResult> Notify([FromBody] NotifyRequestDto payload) 
        => await Mediator.Send(NotificationsWebMapper.MapToNotifyRequestCommand(payload));

    /// <summary>
    /// Returns preserved notification for given Status ID. It is recommended to check notification
    /// separately via HTTP request in case web sockets fails.
    /// </summary>
    /// <param name="payload">Status ID.</param>
    /// <returns>Data payload including user ID, optional payload and notification method.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(StatusRequestCommandResult),StatusCodes.Status200OK)]
    public async Task<StatusRequestCommandResult> Status([FromBody] StatusRequestDto payload) 
        => await Mediator.Send(NotificationsWebMapper.MapToStatusRequestCommand(payload));
}