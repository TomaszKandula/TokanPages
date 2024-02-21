using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Application.NotificationsWeb.Command;
using TokanPages.Gateway.Dto.NotificationsWeb;

namespace TokanPages.Gateway.Controllers.Mappers;

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
        Payload = model.Payload,
        Handler = model.Handler
    };
}