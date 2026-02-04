using Microsoft.Extensions.Options;
using TokanPages.Backend.Configuration.Options;
using TokanPages.Backend.Domain.Entities;
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

    public async Task<bool> CreateServiceBusMessage(Guid messageId)
    {
        try
        {
            var entity = new ServiceBusMessage
            {
                Id = messageId,
                IsConsumed = false
            };

            await DbOperations.Insert(entity);
        }
        catch
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateServiceBusMessage(Guid messageId, bool isConsumed)
    {
        try
        {
            var filterBy = new {  MessageId = messageId };
            var updateBy = new {  IsConsumed = isConsumed };

            await DbOperations.Update<ServiceBusMessage>(updateBy, filterBy);
        }
        catch
        {
            return false;    
        }

        return true;
    }
}