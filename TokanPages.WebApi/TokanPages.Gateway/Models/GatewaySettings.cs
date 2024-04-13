namespace TokanPages.Gateway.Models;

/// <summary>
/// Gateway settings.
/// </summary>
public class GatewaySettings
{
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