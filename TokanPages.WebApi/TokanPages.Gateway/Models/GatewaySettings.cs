namespace TokanPages.Gateway.Models;

/// <summary>
/// 
/// </summary>
public class GatewaySettings
{
    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<RouteDefinition>? Routes { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public RouteDefaults? Defaults { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public RequestMessageHeaders? Headers { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public ResponseMessageHeaders? ResponseHeaders { get; set; }
}