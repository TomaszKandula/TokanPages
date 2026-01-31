namespace TokanPages.Gateway.Models;

/// <summary>
/// Gateway settings.
/// </summary>
public class GatewaySettingsModel
{
    /// <summary>
    /// Configuration section name.
    /// </summary>
    public const string SectionName = "GatewaySettings";

    /// <summary>
    /// Allowed path(s).
    /// </summary>
    public string Allowed { get; set; } = string.Empty;

    /// <summary>
    /// List of routes.
    /// </summary>
    public IEnumerable<RouteDefinition>? Routes { get; set; }

    /// <summary>
    /// Default settings.
    /// </summary>
    public RouteDefaults? Defaults { get; set; }

    /// <summary>
    /// Headers.
    /// </summary>
    public RequestMessageHeaders? Headers { get; set; }

    /// <summary>
    /// Response headers.
    /// </summary>
    public ResponseMessageHeaders? ResponseHeaders { get; set; }
}