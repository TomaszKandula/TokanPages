using System.Diagnostics.CodeAnalysis;
using TokanPages.HostedServices.Services.Abstractions;

namespace TokanPages.HostedServices.Services.Models;

/// <summary>
/// Caching processing configuration.
/// </summary>
[ExcludeFromCodeCoverage]
public class CachingProcessingConfig : ICachingProcessingConfig
{
    /// <summary>
    /// CRON expression.
    /// </summary>
    public string CronExpression { get; set; } = "";

    /// <summary>
    /// Time zone information. Local is default value.
    /// </summary>
    public TimeZoneInfo TimeZoneInfo { get; set; } = TimeZoneInfo.Local;

    /// <summary>
    /// List of URLs of web pages to be cached.
    /// </summary>
    public List<RoutePath> RoutePaths { get; set; } = new();
}