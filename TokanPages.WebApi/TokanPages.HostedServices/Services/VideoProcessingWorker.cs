using Microsoft.Extensions.Hosting;
using TokanPages.Backend.Core.Utilities.LoggerService;

namespace TokanPages.HostedServices.Services;

/// <summary>
/// Video Processing Worker using Hosted Service.
/// </summary>
public class VideoProcessingWorker : IHostedService, IDisposable
{
    private readonly ILoggerService _loggerService;

    private readonly VideoProcessing _videoProcessing;

    /// <summary>
    /// Video Processing Worker.
    /// </summary>
    /// <param name="loggerService">Logger instance.</param>
    /// <param name="videoProcessing">Video processing service instance.</param>
    public VideoProcessingWorker(ILoggerService loggerService, VideoProcessing videoProcessing)
    {
        _loggerService = loggerService;
        _videoProcessing = videoProcessing;
    }

    /// <summary>
    /// Starting the service.
    /// </summary>
    /// <param name="cancellationToken"></param>
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _loggerService.LogInformation("Starting the service bus queue consumer for video processing.");
        await _videoProcessing.RegisterOnMessageHandlerAndReceiveMessages().ConfigureAwait(false);
    }

    /// <summary>
    /// Stopping the service.
    /// </summary>
    /// <param name="cancellationToken"></param>
    public async Task StopAsync(CancellationToken cancellationToken)
    {
        _loggerService.LogInformation("Stopping the service bus queue consumer for video processing.");
        await _videoProcessing.CloseQueueAsync().ConfigureAwait(false);
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
            await _videoProcessing.DisposeAsync().ConfigureAwait(false);
    }
}