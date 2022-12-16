using System.Diagnostics.CodeAnalysis;

namespace TokanPages.WebApi.Dto.NotificationsWeb;

/// <summary>
/// Notification payload definition.
/// </summary>
[ExcludeFromCodeCoverage]
public class NotifyRequestDto
{
    /// <summary>
    /// Optional notification payload.
    /// </summary>
    public object? Payload { get; set; }

    /// <summary>
    /// Handler name to be called by the WSS.
    /// </summary>
    public string Handler { get; set; } = "";
}