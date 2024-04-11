using Azure.Messaging.ServiceBus;

namespace TokanPages.Services.AzureBusService.Abstractions;

public interface IAzureBusClient
{
    ServiceBusProcessor GetProcessor(string queueName, ServiceBusProcessorOptions options);

    Task SendMessages(string queueName, IEnumerable<string> messages, CancellationToken cancellationToken = default);
}