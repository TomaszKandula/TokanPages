using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Notifications.Dto.NotificationsMobile;

/// <summary>
/// Use it to send push notification.
/// </summary>
public class SendNotificationDto
{
    /// <summary>
    /// Mandatory.
    /// </summary>
    public PlatformType Platform { get; set; }

    /// <summary>
    /// Mandatory.
    /// </summary>
    public string MessageTitle { get; set; } = "";

    /// <summary>
    /// Mandatory.
    /// </summary>
    public string MessageBody { get; set; } = "";

    /// <summary>
    /// Optional field.
    /// </summary>
    public string[]? Tags { get; set; }
}