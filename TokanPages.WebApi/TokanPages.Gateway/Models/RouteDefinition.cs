namespace TokanPages.Gateway.Models;

/// <summary>
/// Route properties.
/// </summary>
public class RouteDefinition
{
    /// <summary>
    /// Path.
    /// </summary>
    public string Path { get; set; } = "";

    /// <summary>
    /// Service name.
    /// </summary>
    public string ServiceName { get; set; } = "";

    /// <summary>
    /// Schema.
    /// </summary>
    public string Schema { get; set; }  = "http";

    /// <summary>
    /// Host.
    /// </summary>
    public string Host { get; set; } = "localhost";

    /// <summary>
    /// Port.
    /// </summary>
    public string? Port { get; set; } = "80";
}