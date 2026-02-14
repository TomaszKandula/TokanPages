using System.Diagnostics.CodeAnalysis;
using Cronos;
using TokanPages.Backend.Utility.Abstractions;

namespace TokanPages.HostedServices.Base;

/// <summary>
/// Cron job base implementation using threading timer.
/// </summary>
[ExcludeFromCodeCoverage]
public class CronJob : IHostedService, IDisposable
{
    private Timer? _timer;

    private readonly CronExpression _expression;

    private readonly TimeZoneInfo _timeZoneInfo;

    /// <summary>
    /// Logger service.
    /// </summary>
    protected readonly ILoggerService LoggerService;

    /// <summary>
    /// Service scope factory to get required service, so the appropriate method can be called.
    /// </summary>
    protected readonly IServiceScopeFactory ServiceScopeFactory;

    /// <summary>
    /// Cron job implementation using threading timer.
    /// </summary>
    /// <param name="cronExpression">CRON expression.</param>
    /// <param name="timeZoneInfo">Time Zone info instance.</param>
    /// <param name="loggerService">Logger service instance.</param>
    /// <param name="serviceScopeFactory">Service scope factory instance.</param>
    protected CronJob(string cronExpression, TimeZoneInfo timeZoneInfo, ILoggerService loggerService, IServiceScopeFactory serviceScopeFactory)
    {
        _expression = CronExpression.Parse(cronExpression);
        _timeZoneInfo = timeZoneInfo;
        ServiceScopeFactory = serviceScopeFactory;
        LoggerService = loggerService;
    }

    /// <summary>
    /// Returns created service for given type.
    /// </summary>
    /// <typeparam name="T">Given type to be created as scoped.</typeparam>
    /// <exception cref="ArgumentNullException">Throws as exception if service is null.</exception>
    /// <returns>Created service.</returns>
    public T GetService<T>() where T : notnull
    {
        using var scope = ServiceScopeFactory.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<T>();

        ArgumentNullException.ThrowIfNull(service);

        return service;
    }

    /// <summary>
    /// Disposing service.
    /// </summary>
    /// <param name="canDispose"></param>
    protected void Dispose(bool canDispose)
    {
        if (canDispose && _timer != null)
            _timer.Dispose();
    }

    /// <summary>
    /// Schedule CRON job.
    /// </summary>
    /// <param name="cancellationToken"></param>
    private async Task ScheduleJob(CancellationToken cancellationToken)
    {
        var offset = _expression.GetNextOccurrence(DateTimeOffset.Now, _timeZoneInfo);
        if (!offset.HasValue)
        {
            return;
        }

        var delay = offset.Value - DateTimeOffset.Now;
        if (delay.TotalMilliseconds <= 0)
            await ScheduleJob(cancellationToken);

        _timer = new Timer(Callback, null, delay, Timeout.InfiniteTimeSpan);

        await Task.CompletedTask;
        return;

        //TODO: consider change the entire implementation to avoid 'async void'
        async void Callback(object? _)
        {
            _timer?.Dispose();
            _timer = null;

            if (cancellationToken.IsCancellationRequested)
                return;

            await DoWork(cancellationToken);
            await ScheduleJob(cancellationToken);
        }
    }

    /// <summary>
    /// Work implementation.
    /// </summary>
    /// <param name="cancellationToken"></param>
    public virtual async Task DoWork(CancellationToken cancellationToken)
    {
        await Task.Delay(5000, cancellationToken);
    }

    /// <summary>
    /// Start timer.
    /// </summary>
    /// <param name="cancellationToken"></param>
    public virtual async Task StartAsync(CancellationToken cancellationToken)
    {
        await ScheduleJob(cancellationToken);
    }

    /// <summary>
    /// Stop timer.
    /// </summary>
    /// <param name="cancellationToken"></param>
    public virtual async Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, Timeout.Infinite);
        await Task.CompletedTask;
    }

    /// <summary>
    /// Disposing.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}