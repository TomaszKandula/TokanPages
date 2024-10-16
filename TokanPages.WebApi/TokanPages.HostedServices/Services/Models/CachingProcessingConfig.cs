using System.Diagnostics.CodeAnalysis;
using TokanPages.HostedServices.Services.Abstractions;

namespace TokanPages.HostedServices.Services.Models;

/// <summary>
/// Caching processing configuration.
/// </summary>
[ExcludeFromCodeCoverage]
public class CachingProcessingConfig : ICachingProcessingConfig
{
    /// <inheritdoc />
    public string CronExpression { get; set; } = "";

    /// <inheritdoc />
    public TimeZoneInfo TimeZoneInfo { get; set; } = TimeZoneInfo.Local;

    /// <inheritdoc />
    public string? BaseUrl { get; set; }

    /// <inheritdoc />
    public string[]? FilesToCache { get; set; }

    /// <inheritdoc />
    public List<RoutePath> RoutePaths { get; set; } = new();
}