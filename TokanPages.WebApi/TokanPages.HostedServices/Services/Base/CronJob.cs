using Cronos;

namespace TokanPages.HostedServices.Services.Base;

/// <summary>
/// Cron job base implementation using threading timer.
/// </summary>
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
    /// Schedule CRON job.
    /// </summary>
    /// <param name="cancellationToken"></param>
    private async Task ScheduleJob(CancellationToken cancellationToken)
    {
        var offset = _expression.GetNextOccurrence(DateTimeOffset.Now, _timeZoneInfo);
        if (offset.HasValue)
        {
            var delay = offset.Value - DateTimeOffset.Now;
            if (delay.TotalMilliseconds <= 0)
                await ScheduleJob(cancellationToken);

            async void Callback(object? _)
            {
                _timer?.Dispose();
                _timer = null;

                if (!cancellationToken.IsCancellationRequested)
                    await DoWork(cancellationToken);

                if (!cancellationToken.IsCancellationRequested)
                    await ScheduleJob(cancellationToken);
            }

            _timer = new Timer(Callback, null, delay, new TimeSpan(0, 0, 0, 0, -1));            
        }

        await Task.CompletedTask;
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

    /// <summary>
    /// Disposing service.
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual async void Dispose(bool disposing)
    {
        if (disposing && _timer != null)
            await _timer.DisposeAsync().ConfigureAwait(false);
    }
}