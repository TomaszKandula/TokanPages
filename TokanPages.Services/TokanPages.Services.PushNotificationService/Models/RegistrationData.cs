using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Services.PushNotificationService.Models;

/// <summary>
/// Represents the result of the registration
/// </summary>
[ExcludeFromCodeCoverage]
public class RegistrationData
{
    /// <summary>
    /// Registration ID
    /// </summary>
    public string RegistrationId { get; set; } = "";

    /// <summary>
    /// PNS handle value
    /// </summary>
    public string PnsHandle { get; set; } = "";

    /// <summary>
    /// Application platform (only Apple/Google supported)
    /// </summary>
    public string ApplicationPlatform { get; set; } = "";
}