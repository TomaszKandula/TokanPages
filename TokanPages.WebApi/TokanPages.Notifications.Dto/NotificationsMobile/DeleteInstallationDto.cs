using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Notifications.Dto.NotificationsMobile;

/// <summary>
/// Use it when you want to remove existing installation ID.
/// </summary>
[ExcludeFromCodeCoverage]
public class DeleteInstallationDto
{
    /// <summary>
    /// Installation ID.
    /// </summary>
    public Guid Id { get; set; }
}