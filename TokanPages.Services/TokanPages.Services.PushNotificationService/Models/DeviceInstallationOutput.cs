using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Services.PushNotificationService.Models;

[ExcludeFromCodeCoverage]
public class DeviceInstallationOutput
{
    public string InstallationId { get; set; } = "";

    public string RegistrationId { get; set; } = "";

    public string PnsHandle { get; set; } = "";

    public string? ExpirationTime { get; set; }

    public string Platform { get; set; } = "";

    public string[]? Tags { get; set; }
}