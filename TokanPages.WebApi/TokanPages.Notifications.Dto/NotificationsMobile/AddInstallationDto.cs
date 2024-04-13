using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Notifications.Dto.NotificationsMobile;

/// <summary>
/// Use it when you want to register for Azure Notification Hub.
/// </summary>
[ExcludeFromCodeCoverage]
public class AddInstallationDto
{
    /// <summary>
    /// Mandatory.
    /// </summary>
    public string PnsHandle { get; set; } = "";

    /// <summary>
    /// Mandatory.
    /// </summary>
    public PlatformType Platform { get; set; }

    /// <summary>
    /// Mandatory.
    /// </summary>
    public string[] Tags { get; set; } = Array.Empty<string>();
}