namespace TokanPages.HostedServices.Services.Models;

/// <summary>
/// Route path definition.
/// </summary>
public class RoutePath
{
    /// <summary>
    /// Fully qualified path.
    /// </summary>
    public string Url { get; set; } = "";

    /// <summary>
    /// Page name (ie. main, about etc).
    /// </summary>
    public string Name { get; set; } = "";
}