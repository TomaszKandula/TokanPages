using System.Diagnostics.CodeAnalysis;
using Azure.Messaging.ServiceBus;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.HostedServices.Base.Abstractions;
using TokanPages.Persistence.DataAccess.Repositories.Messaging;
using TokanPages.Services.AzureBusService.Abstractions;

namespace TokanPages.HostedServices.Base;

/// <summary>
/// Processing contract implementation.
/// </summary>
[ExcludeFromCodeCoverage]
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
    
    /// <summary>
    /// Messaging repository.
    /// </summary>
    protected readonly IMessagingRepository MessagingRepository;

    /// <summary>
    /// Service factory.
    /// </summary>
    protected readonly IServiceScopeFactory ServiceScopeFactory;
    
    private ServiceBusProcessor? _processor;

    /// <summary>
    /// Processing contract implementation.
    /// </summary>
    /// <param name="loggerService">Logger instance.</param>
    /// <param name="azureBusFactory">Azure Bus Factory instance.</param>
    /// <param name="messagingRepository">Messaging repository instance.</param>
    /// <param name="serviceScopeFactory">Service scope factory instance.</param>
    protected Processing(ILoggerService loggerService, IAzureBusFactory azureBusFactory, 
        IMessagingRepository messagingRepository, IServiceScopeFactory serviceScopeFactory)
    {
        LoggerService = loggerService;
        AzureBusFactory = azureBusFactory;
        MessagingRepository = messagingRepository;
        ServiceScopeFactory = serviceScopeFactory;
    }

    /// <inheritdoc />
    public T GetService<T>() where T : notnull
    {
        using var scope = ServiceScopeFactory.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<T>();

        ArgumentNullException.ThrowIfNull(service);

        return service;
    }

    /// <inheritdoc />
    public async Task<bool> CanContinue(Guid messageId)
    {
        var busMessages = await MessagingRepository.GetServiceBusMessage(messageId);
        if (busMessages is null)
            return false;

        await MessagingRepository.DeleteServiceBusMessage(messageId);
        return true;
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