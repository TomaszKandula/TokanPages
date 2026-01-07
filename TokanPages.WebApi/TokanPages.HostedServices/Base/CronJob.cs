using System.Diagnostics.CodeAnalysis;
using Cronos;

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
    /// Cron job implementation using threading timer.
    /// </summary>
    /// <param name="cronExpression">CRON expression.</param>
    /// <param name="timeZoneInfo">Time Zone info instance.</param>
    protected CronJob(string cronExpression, TimeZoneInfo timeZoneInfo)
    {
        _expression = CronExpression.Parse(cronExpression);
        _timeZoneInfo = timeZoneInfo;
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

        void Callback(object? _)
        {
            _timer?.Dispose();
            _timer = null;

            if (!cancellationToken.IsCancellationRequested)
                Task.Run(() => DoWork(cancellationToken), cancellationToken); 

            if (!cancellationToken.IsCancellationRequested)
                Task.Run(() => ScheduleJob(cancellationToken), cancellationToken);
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