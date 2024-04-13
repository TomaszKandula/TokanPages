using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Application.NotificationsWeb.Command;
using TokanPages.Notifications.Dto.NotificationsWeb;

namespace TokanPages.Notifications.Controllers.Mappers;

/// <summary>
/// Notifications mapper.
/// </summary>
[ExcludeFromCodeCoverage]
public static class NotificationsWebMapper
{
    /// <summary>
    /// Map request DTO to a given command.
    /// </summary>
    /// <param name="model">Notifications object.</param>
    /// <returns>Command object.</returns>
    public static NotifyRequestCommand MapToNotifyRequestCommand(NotifyRequestDto model) => new()
    {
        UserId = model.UserId,
        CanSkipPreservation = model.CanSkipPreservation,
        ExternalPayload = model.ExternalPayload,
        Handler = model.Handler
    };

    /// <summary>
    /// Map request DTO to a given command.
    /// </summary>
    /// <param name="model">Status object.</param>
    /// <returns>Command object.</returns>
    public static StatusRequestCommand MapToStatusRequestCommand(StatusRequestDto model) => new()
    {
        StatusId = model.StatusId
    };
}