using TokanPages.Backend.Core.Utilities.LoggerService;

namespace TokanPages.HostedServices.Services;

/// <summary>
/// Email Processing Worker using Hosted Service.
/// </summary>
public class EmailProcessingWorker : IHostedService, IDisposable
{
    private readonly ILoggerService _loggerService;

    private readonly EmailProcessing _emailProcessing;

    /// <summary>
    /// Video Processing Worker.
    /// </summary>
    /// <param name="loggerService">Logger instance.</param>
    /// <param name="emailProcessing">Video processing service instance.</param>
    public EmailProcessingWorker(ILoggerService loggerService, EmailProcessing emailProcessing)
    {
        _loggerService = loggerService;
        _emailProcessing = emailProcessing;
    }

    /// <summary>
    /// Starting the service.
    /// </summary>
    /// <param name="cancellationToken"></param>
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _loggerService.LogInformation("Starting the service bus queue consumer for email processing.");
        await _emailProcessing.RegisterOnMessageHandlerAndReceiveMessages().ConfigureAwait(false);
    }

    /// <summary>
    /// Stopping the service.
    /// </summary>
    /// <param name="cancellationToken"></param>
    public async Task StopAsync(CancellationToken cancellationToken)
    {
        _loggerService.LogInformation("Stopping the service bus queue consumer for email processing.");
        await _emailProcessing.CloseQueueAsync().ConfigureAwait(false);
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
            await _emailProcessing.DisposeAsync().ConfigureAwait(false);
    }
}