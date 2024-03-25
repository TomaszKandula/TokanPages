namespace TokanPages.Gateway.Models;

/// <summary>
/// 
/// </summary>
public class RouteDefinition
{
    /// <summary>
    /// 
    /// </summary>
    public string Path { get; set; } = "";

    /// <summary>
    /// 
    /// </summary>
    public string ServiceName { get; set; } = "";

    /// <summary>
    /// 
    /// </summary>
    public string Schema { get; set; }  = "http";

    /// <summary>
    /// 
    /// </summary>
    public string Host { get; set; } = "localhost";

    /// <summary>
    /// 
    /// </summary>
    public string? Port { get; set; } = "80";
}