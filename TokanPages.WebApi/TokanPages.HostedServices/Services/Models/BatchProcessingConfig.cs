using TokanPages.HostedServices.Services.Abstractions;

namespace TokanPages.HostedServices.Services.Models;

/// <summary>
/// Batch processing configuration.
/// </summary>
public class BatchProcessingConfig : IBatchProcessingConfig
{
    /// <summary>
    /// CRON expression.
    /// </summary>
    public string CronExpression { get; set; }

    /// <summary>
    /// Time zone information. Local is default value.
    /// </summary>
    public TimeZoneInfo TimeZoneInfo { get; set; }
}