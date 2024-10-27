using System.Diagnostics.CodeAnalysis;
using TokanPages.HostedServices.CronJobs.Abstractions;

namespace TokanPages.HostedServices.CronJobs;

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