using TokanPages.HostedServices.Services.Models;

namespace TokanPages.HostedServices.Services.Abstractions;

/// <inheritdoc />
public interface ICachingProcessingConfig : IBaseConfig
{
    /// <summary>
    /// List of URLs of web pages to be cached.
    /// </summary>
    public List<RoutePath> RoutePaths { get; set; }
}
