using Azure.Messaging.ServiceBus;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Services.AzureBusService.Abstractions;

namespace TokanPages.Services.AzureBusService;

public class AzureBusClient : IAzureBusClient
{
    private readonly ServiceBusClient _client;

    public AzureBusClient(string connectionString)
    {
        var clientOptions = new ServiceBusClientOptions { TransportType = ServiceBusTransportType.AmqpWebSockets };
        _client = new ServiceBusClient(connectionString, clientOptions);
    }

    public ServiceBusProcessor GetProcessor(string queueName, ServiceBusProcessorOptions options)
    {
        return _client.CreateProcessor(queueName, options);
    }

    /// <summary>
    /// Sends given message to the Azure Service Bus for processing. 
    /// </summary>
    /// <remarks>
    /// To make sure that the message will be picked up and processed,
    /// try to send it more then once. The hosted service worker should
    /// discard any doubled message.
    /// </remarks>
    /// <param name="queueName">Registered queue name with the Azure Bus Service.</param>
    /// <param name="messages">List of messages to be sent.</param>
    /// <param name="cancellationToken">Passed cancellation token.</param>
    /// <exception cref="GeneralException">Throws BUS_MESSAGE_TOO_LARGE if message exceeds the limit.</exception>
    public async Task SendMessages(string queueName, IEnumerable<string> messages, CancellationToken cancellationToken = default)
    {
        var sender = _client.CreateSender(queueName);
        try
        {
            using var messageBatch = await sender.CreateMessageBatchAsync(cancellationToken);
            if (messages.Select(message => new ServiceBusMessage(message)).Any(newMessage => !messageBatch.TryAddMessage(newMessage)))
                throw new GeneralException(nameof(ErrorCodes.BUS_MESSAGE_TOO_LARGE), ErrorCodes.BUS_MESSAGE_TOO_LARGE);

            const int count = 3;
            for (var index = 0; index < count; index++)
            {
                await sender.SendMessagesAsync(messageBatch, cancellationToken);
                await Task.Delay(500, cancellationToken);
            }
        }
        finally
        {
            await sender.DisposeAsync();
            await _client.DisposeAsync();            
        }
    }
}