namespace TokanPages.HostedServices.Services.Abstractions;

/// <summary>
/// Schedule configuration contract.
/// </summary>
public interface IBaseConfig
{
    /// <summary>
    /// CRON expression (format: "* * * * *"). Explanation:
    /// <list type="bullet">
    /// <item><description>1st argument: Minutes (0-59)</description></item>
    /// <item><description>2nd argument: Hour (0-23)</description></item>
    /// <item><description>3rd argument: Day of the Month (1-31)</description></item>
    /// <item><description>4th argument: Month of the Year (1-12)</description></item>
    /// <item><description>5th argument: Day of the Week (0-6)</description></item>
    /// </list>
    /// Examples:
    /// <list type="bullet">
    /// <item><description>5 0 * 8 * = At 00:05 in August.</description></item>
    /// <item><description>5 4 * * 6 = At 04:05 on Saturday.</description></item>
    /// <item><description>0 22 * * 1-5 = At 22:00 on every day-of-week from Monday through Friday.</description></item>
    /// </list>
    /// </summary>
    string CronExpression { get; set; }

    /// <summary>
    /// Time zone instance.
    /// </summary>
    TimeZoneInfo TimeZoneInfo { get; set; }
}