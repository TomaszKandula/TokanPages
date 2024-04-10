using TokanPages.HostedServices.Services.Abstractions;

namespace TokanPages.HostedServices.Services.Models;

/// <summary>
/// 
/// </summary>
public class BatchProcessingConfig : IBatchProcessingConfig
{
    /// <summary>
    /// 
    /// </summary>
    public string CronExpression { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public TimeZoneInfo TimeZoneInfo { get; set; }
}