using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Application.NotificationsMobile.Commands;
using TokanPages.Notifications.Dto.NotificationsMobile;

namespace TokanPages.Notifications.Controllers.Mappers;

/// <summary>
/// Notifications mapper.
/// </summary>
[ExcludeFromCodeCoverage]
public static class NotificationsMapper
{
    /// <summary>
    /// Map request DTO to a given command.
    /// </summary>
    /// <param name="model">Notifications object.</param>
    /// <returns>Command object.</returns>
    public static AddInstallationCommand MapToAddInstallationCommand(AddInstallationDto model) => new()
    {
        PnsHandle = model.PnsHandle,
        Platform = model.Platform,
        Tags = model.Tags
    };

    /// <summary>
    /// Map request DTO to a given command.
    /// </summary>
    /// <param name="model">Notifications object.</param>
    /// <returns>Command object.</returns>
    public static SendNotificationCommand MapToSendNotificationCommand(SendNotificationDto model) => new()
    {
        Platform = model.Platform,
        MessageTitle = model.MessageTitle,
        MessageBody = model.MessageBody,
        Tags = model.Tags
    };

    /// <summary>
    /// Map request DTO to a given command.
    /// </summary>
    /// <param name="model">Notifications object.</param>
    /// <returns>Command object.</returns>
    public static DeleteInstallationCommand MapToDeleteInstallationCommand(DeleteInstallationDto model) => new()
    {
        Id = model.Id
    };
}