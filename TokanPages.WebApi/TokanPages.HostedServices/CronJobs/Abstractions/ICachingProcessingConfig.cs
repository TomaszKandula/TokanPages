using TokanPages.HostedServices.Base.Abstractions;
using TokanPages.HostedServices.Models;

namespace TokanPages.HostedServices.CronJobs.Abstractions;

/// <inheritdoc />
public interface ICachingProcessingConfig : IBaseConfig
{
    /// <summary>
    /// Base URL of the source.
    /// </summary>
    public string GetActionUrl { get; set; }

    /// <summary>
    /// URL path to POST endpoint.
    /// </summary>
    public string PostActionUrl { get; set; }

    /// <summary>
    /// List of files to be cached.
    /// </summary>
    public string[]? FilesToCache { get; set; }

    /// <summary>
    /// List of URLs of web pages to be cached.
    /// </summary>
    public List<RoutePath> RoutePaths { get; set; }
}
