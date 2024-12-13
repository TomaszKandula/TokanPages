using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Core.Utilities.LoggerService;

namespace TokanPages.HostedServices.Workers;

/// <summary>
/// SPA cache processing worker using hosted service.
/// </summary>
[ExcludeFromCodeCoverage]
public class CacheProcessingWorker : IHostedService, IDisposable
{
    private readonly ILoggerService _loggerService;

    private readonly CacheProcessing _cacheProcessing;

    /// <summary>
    /// Cache processing worker.
    /// </summary>
    /// <param name="loggerService"></param>
    /// <param name="cacheProcessing"></param>
    public CacheProcessingWorker(ILoggerService loggerService, CacheProcessing cacheProcessing)
    {
        _loggerService = loggerService;
        _cacheProcessing = cacheProcessing;
    }

    /// <summary>
    /// Starting the service.
    /// </summary>
    /// <param name="cancellationToken"></param>
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _loggerService.LogInformation("Starting the service bus queue consumer for SPA cache processing.");
        await _cacheProcessing.RegisterOnMessageHandlerAndReceiveMessages().ConfigureAwait(false);
    }

    /// <summary>
    /// Stopping the service.
    /// </summary>
    /// <param name="cancellationToken"></param>
    public async Task StopAsync(CancellationToken cancellationToken)
    {
        _loggerService.LogInformation("Stopping the service bus queue consumer for SPA cache processing.");
        await _cacheProcessing.CloseQueueAsync().ConfigureAwait(false);
    }

    /// <summary>
    /// Worker disposing.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Service disposing.
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual async void Dispose(bool disposing)
    {
        if (disposing)
            await _cacheProcessing.DisposeAsync().ConfigureAwait(false);
    }
}