using Azure.Messaging.ServiceBus;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.HostedServices.Services.Abstractions;
using TokanPages.Services.AzureBusService.Abstractions;

namespace TokanPages.HostedServices.Services.Base;

/// <summary>
/// Processing contract implementation.
/// </summary>
public abstract class Processing : IProcessing
{
    /// <summary>
    /// Error for empty message. Default value is "Empty body request.". 
    /// </summary>
    protected virtual string ErrorNoRequestBody { get; set;  } = "Empty body request.";

    /// <summary>
    /// Bus service queue name. Default value is "defaultQueue".
    /// </summary>
    protected virtual string QueueName { get; set; } = "defaultQueue";

    /// <summary>
    /// The maximum number of concurrent calls to the message handler. The default value is 1.
    /// </summary>
    protected virtual int MaxConcurrentCalls { get; set; } = 1;

    /// <summary>
    /// true to complete the message automatically on successful execution of the message handler;
    /// otherwise, false. The default value is true.
    /// </summary>
    protected virtual bool AutoCompleteMessages { get; set; } = true;

    /// <summary>
    /// The mode to use for receiving messages. The default value is "PeekLock".
    /// </summary>
    protected virtual ServiceBusReceiveMode ReceiveMode { get; set; } = ServiceBusReceiveMode.PeekLock;

    /// <summary>
    /// Logger Service.
    /// </summary>
    protected readonly ILoggerService LoggerService;

    /// <summary>
    /// Azure Bus Factory.
    /// </summary>
    protected readonly IAzureBusFactory AzureBusFactory;

    private ServiceBusProcessor? _processor;

    /// <summary>
    /// Processing contract implementation.
    /// </summary>
    /// <param name="loggerService">Logger instance.</param>
    /// <param name="azureBusFactory">Azure Bus Factory instance.</param>
    protected Processing(ILoggerService loggerService, IAzureBusFactory azureBusFactory)
    {
        LoggerService = loggerService;
        AzureBusFactory = azureBusFactory;
    }

    /// <inheritdoc />
    public virtual async Task RegisterOnMessageHandlerAndReceiveMessages()
    {
        var busService = AzureBusFactory.Create();
        var serviceBusProcessorOptions = new ServiceBusProcessorOptions
        {
            MaxConcurrentCalls = MaxConcurrentCalls,
            AutoCompleteMessages = AutoCompleteMessages,
            ReceiveMode = ReceiveMode
        };

        _processor = busService.GetProcessor(QueueName, serviceBusProcessorOptions);
        _processor.ProcessMessageAsync += ProcessMessagesAsync;
        _processor.ProcessErrorAsync += ProcessErrorAsync;
        await _processor.StartProcessingAsync().ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task CloseQueueAsync()
    {
        await _processor!.CloseAsync().ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async ValueTask DisposeAsync()
    {
        if (_processor != null)
            await _processor.DisposeAsync().ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual Task ProcessMessagesAsync(ProcessMessageEventArgs args)
    {
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public virtual Task ProcessErrorAsync(ProcessErrorEventArgs arg)
    {
        LoggerService.LogError($"Message handler encountered an exception: {arg.Exception}");
        LoggerService.LogDebug($"- ErrorSource: {arg.ErrorSource}");
        LoggerService.LogDebug($"- Entity Path: {arg.EntityPath}");
        LoggerService.LogDebug($"- FullyQualifiedNamespace: {arg.FullyQualifiedNamespace}");

        return Task.CompletedTask;
    }
}