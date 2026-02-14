using Microsoft.Extensions.Options;
using TokanPages.Backend.Domain.Entities;
using TokanPages.Backend.Shared.Options;
using TokanPages.Persistence.DataAccess.Abstractions;

namespace TokanPages.Persistence.DataAccess.Repositories.Messaging;

public class MessagingRepository : RepositoryBase, IMessagingRepository
{
    public MessagingRepository(IDbOperations dbOperations, IOptions<AppSettingsModel> appSettings) 
        : base(dbOperations, appSettings) { }

    public async Task<ServiceBusMessage?> GetServiceBusMessage(Guid messageId)
    {
        var filterBy = new {  MessageId = messageId };
        var data = (await DbOperations.Retrieve<ServiceBusMessage>(filterBy)).SingleOrDefault();
        return data ?? null;
    }

    public async Task CreateServiceBusMessage(Guid messageId)
    {
        var entity = new ServiceBusMessage
        {
            Id = messageId,
            IsConsumed = false
        };

        await DbOperations.Insert(entity);
    }

    public async Task UpdateServiceBusMessage(Guid messageId, bool isConsumed)
    {
        var updateBy = new { IsConsumed = isConsumed };
        var filterBy = new { MessageId = messageId };

        await DbOperations.Update<ServiceBusMessage>(updateBy, filterBy);
    }

    public async Task RemoveServiceBusMessage(Guid messageId)
    {
        var deleteBy = new { Id = messageId };
        await DbOperations.Delete<ServiceBusMessage>(deleteBy);
    }
}