using System.Diagnostics.CodeAnalysis;
using TokanPages.HostedServices.Services.Abstractions;

namespace TokanPages.HostedServices.Services.Models;

/// <summary>
/// Batch processing configuration.
/// </summary>
[ExcludeFromCodeCoverage]
public class BatchProcessingConfig : IBatchProcessingConfig
{
    /// <inheritdoc />
    public string CronExpression { get; set; } = "";

    /// <inheritdoc />
    public TimeZoneInfo TimeZoneInfo { get; set; } = TimeZoneInfo.Local;
}