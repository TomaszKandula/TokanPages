using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Notifications.Dto.NotificationsWeb;

/// <summary>
/// Notification payload definition.
/// </summary>
[ExcludeFromCodeCoverage]
public class NotifyRequestDto
{
    /// <summary>
    /// User ID.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Optional, allows to skip notification preservation. 
    /// </summary>
    public bool CanSkipPreservation { get; set; }

    /// <summary>
    /// Optional notification payload.
    /// </summary>
    public string? ExternalPayload { get; set; }

    /// <summary>
    /// Method name to be called by the WSS.
    /// </summary>
    public string Handler { get; set; } = "";
}