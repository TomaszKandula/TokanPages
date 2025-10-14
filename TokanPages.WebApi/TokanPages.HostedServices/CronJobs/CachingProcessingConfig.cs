using System.Diagnostics.CodeAnalysis;
using TokanPages.HostedServices.CronJobs.Abstractions;
using TokanPages.HostedServices.Models;

namespace TokanPages.HostedServices.CronJobs;

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
    public string? GetActionUrl { get; set; }

    /// <inheritdoc />
    public string? PostActionUrl { get; set; }

    /// <inheritdoc />
    public string[]? FilesToCache { get; set; }

    /// <inheritdoc />
    public List<RoutePath> RoutePaths { get; set; } = new();

    /// <inheritdoc />
    public List<RoutePath> PdfRoutePaths { get; set; } = new();
}