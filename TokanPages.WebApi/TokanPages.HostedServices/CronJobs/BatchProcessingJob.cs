using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.HostedServices.Base;
using TokanPages.HostedServices.CronJobs.Abstractions;
using TokanPages.Services.BatchService.Abstractions;

namespace TokanPages.HostedServices.CronJobs;

/// <summary>
/// CRON job implementation.
/// </summary>
[ExcludeFromCodeCoverage]
public class BatchProcessingJob : CronJob
{
    private const string ServiceName = $"[{nameof(BatchProcessingJob)}]";

    private readonly string _cronExpression;

    /// <summary>
    /// CRON job implementation.
    /// </summary>
    /// <param name="config"></param>
    /// <param name="loggerService"></param>
    /// <param name="serviceScopeFactory"></param>
    public BatchProcessingJob(IBatchProcessingConfig config, ILoggerService loggerService, IServiceScopeFactory serviceScopeFactory)
        : base(config.CronExpression, config.TimeZoneInfo, loggerService, serviceScopeFactory) => _cronExpression = config.CronExpression;

    /// <summary>
    /// Execute payment for subscriptions.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task DoWork(CancellationToken cancellationToken)
    {
        LoggerService.LogInformation($"{ServiceName}: working...");
        var batchService = GetService<IBatchService>();
        await batchService.ProcessOutstandingInvoices(cancellationToken);
    }

    /// <summary>
    /// Start CRON job.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override Task StartAsync(CancellationToken cancellationToken)
    {
        LoggerService.LogInformation($"{ServiceName}: started, CRON expression is '{_cronExpression}'.");
        return base.StartAsync(cancellationToken);
    }

    /// <summary>
    /// Stop CRON job.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override Task StopAsync(CancellationToken cancellationToken)
    {
        LoggerService.LogInformation($"{ServiceName}: stopped.");
        return base.StopAsync(cancellationToken);
    }
}