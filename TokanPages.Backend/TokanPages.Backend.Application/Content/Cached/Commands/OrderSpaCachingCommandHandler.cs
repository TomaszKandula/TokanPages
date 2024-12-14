using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TokanPages.Backend.Application.Content.Cached.Commands.Models;
using TokanPages.Backend.Core.Utilities.JsonSerializer;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Entities;
using TokanPages.Persistence.Database;
using TokanPages.Services.AzureBusService.Abstractions;

namespace TokanPages.Backend.Application.Content.Cached.Commands;

public class OrderSpaCachingCommandHandler : RequestHandler<OrderSpaCachingCommand, Unit>
{
    private const string QueueName = "cache_queue";

    private readonly IAzureBusFactory _azureBusFactory;

    private readonly IJsonSerializer _jsonSerializer;

    private static JsonSerializerSettings Settings => new() { ContractResolver = new CamelCasePropertyNamesContractResolver() };

    private static string CurrentEnv => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Testing";

    public OrderSpaCachingCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IAzureBusFactory azureBusFactory, IJsonSerializer jsonSerializer) : base(databaseContext, loggerService)
    {
        _azureBusFactory = azureBusFactory;
        _jsonSerializer = jsonSerializer;
    }

    public override async Task<Unit> Handle(OrderSpaCachingCommand request, CancellationToken cancellationToken)
    {
        var messageId = Guid.NewGuid();
        var serviceBusMessage = new ServiceBusMessage
        {
            Id = messageId,
            IsConsumed = false
        };

        var requestBody = new RequestProcessing
        {
            MessageId = messageId,
            TargetEnv = CurrentEnv,
            GetUrl = request.GetUrl,
            PostUrl = request.PostUrl,
            Files = request.Files,
            Paths = request.Paths
        };

        await DatabaseContext.ServiceBusMessages.AddAsync(serviceBusMessage, cancellationToken);
        await DatabaseContext.SaveChangesAsync(cancellationToken);

        var serialized = _jsonSerializer.Serialize(requestBody, Formatting.None, Settings);
        var messages = new List<string> { serialized };

        LoggerService.LogInformation($"[{QueueName} | {CurrentEnv}]: Send message ({messageId}) to service bus: '{serialized}'.");

        var busService = _azureBusFactory.Create();
        await busService.SendMessages(QueueName, messages, cancellationToken);

        return Unit.Value;
    }
}