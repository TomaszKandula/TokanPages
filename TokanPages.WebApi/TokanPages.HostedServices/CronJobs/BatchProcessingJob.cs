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

    private readonly IBatchService _batchService;

    private readonly ILoggerService _loggerService;

    private readonly string _cronExpression;

    /// <summary>
    /// CRON job implementation.
    /// </summary>
    /// <param name="config"></param>
    /// <param name="batchService"></param>
    /// <param name="loggerService"></param>
    public BatchProcessingJob(IBatchProcessingConfig config, 
        IBatchService batchService, ILoggerService loggerService)
        : base(config.CronExpression, config.TimeZoneInfo)
    {
        _batchService = batchService;
        _loggerService = loggerService;
        _cronExpression = config.CronExpression;
    }

    /// <summary>
    /// Execute payment for subscriptions.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task DoWork(CancellationToken cancellationToken)
    {
        _loggerService.LogInformation($"{ServiceName}: working...");
        await _batchService.ProcessOutstandingInvoices(cancellationToken);
    }

    /// <summary>
    /// Start CRON job.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override Task StartAsync(CancellationToken cancellationToken)
    {
        _loggerService.LogInformation($"{ServiceName}: started, CRON expression is '{_cronExpression}'.");
        return base.StartAsync(cancellationToken);
    }

    /// <summary>
    /// Stop CRON job.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _loggerService.LogInformation($"{ServiceName}: stopped.");
        return base.StopAsync(cancellationToken);
    }
}