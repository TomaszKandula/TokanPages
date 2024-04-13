using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Notifications.Dto.NotificationsWeb;

/// <summary>
/// Status ID of preserved notification.
/// </summary>
[ExcludeFromCodeCoverage]
public class StatusRequestDto
{
    /// <summary>
    /// Status ID.
    /// </summary>
    public Guid StatusId { get; set; }
}