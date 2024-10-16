using TokanPages.HostedServices.Services.Models;

namespace TokanPages.HostedServices.Services.Abstractions;

/// <inheritdoc />
public interface ICachingProcessingConfig : IBaseConfig
{
    /// <summary>
    /// Base URL of the source.
    /// </summary>
    public string? BaseUrl { get; set; }

    /// <summary>
    /// List of files to be cached.
    /// </summary>
    public string[]? FilesToCache { get; set; }

    /// <summary>
    /// List of URLs of web pages to be cached.
    /// </summary>
    public List<RoutePath> RoutePaths { get; set; }
}
