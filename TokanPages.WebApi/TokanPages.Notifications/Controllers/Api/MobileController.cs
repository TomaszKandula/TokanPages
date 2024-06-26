using TokanPages.Backend.Application.Notifications.Mobile.Commands;
using TokanPages.Backend.Application.Notifications.Mobile.Query;
using TokanPages.Notifications.Controllers.Mappers;
using TokanPages.Notifications.Dto.NotificationsMobile;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TokanPages.Notifications.Controllers.Api;

/// <summary>
/// API endpoints definitions for push notifications capability.
/// </summary>
[Authorize]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/notifications/[controller]/[action]")]
public class MobileController : ApiBaseController
{
    /// <inheritdoc />
    public MobileController(IMediator mediator) : base(mediator) { }

    /// <summary>
    /// Returns registered installations.
    /// </summary>
    /// <returns>List of all installations.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(GetInstallationsQueryResult),StatusCodes.Status200OK)]
    public async Task<GetInstallationsQueryResult> GetInstallations() 
        => await Mediator.Send(new GetInstallationsQuery());

    /// <summary>
    /// Returns registered installation by ID.
    /// </summary>
    /// <param name="id">Installation ID.</param>
    /// <returns>Installation details.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(GetInstallationQueryResult),StatusCodes.Status200OK)]
    public async Task<GetInstallationQueryResult> GetInstallation([FromQuery] Guid id) 
        => await Mediator.Send(new GetInstallationQuery { Id = id});

    /// <summary>
    /// Registers PNS handle for Azure Notification Hub.
    /// </summary>
    /// <param name="payload">Contains mandatory PNS handle, target platform and tags.</param>
    /// <returns>MediatR unit value.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(AddInstallationCommandResult),StatusCodes.Status200OK)]
    public async Task<AddInstallationCommandResult> AddInstallation([FromBody] AddInstallationDto payload) 
        => await Mediator.Send(NotificationsMapper.MapToAddInstallationCommand(payload));

    /// <summary>
    /// Removes PNS handle from Azure Notification Hub.
    /// </summary>
    /// <param name="payload">Contains mandatory installation ID.</param>
    /// <returns>MediatR unit value.</returns>
    [HttpDelete]
    [ProducesResponseType(typeof(Unit),StatusCodes.Status200OK)]
    public async Task<Unit> RemoveInstallation([FromBody] DeleteInstallationDto payload) 
        => await Mediator.Send(NotificationsMapper.MapToDeleteInstallationCommand(payload));

    /// <summary>
    /// Allows to send APS/FCM push notification.
    /// </summary>
    /// <param name="payload">Contains platform, message and optional tags.</param>
    /// <returns>Result object.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(SendNotificationCommandResult),StatusCodes.Status200OK)]
    public async Task<SendNotificationCommandResult> SendNotification([FromBody] SendNotificationDto payload) 
        => await Mediator.Send(NotificationsMapper.MapToSendNotificationCommand(payload));
}