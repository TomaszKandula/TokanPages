using TokanPages.Backend.Domain.Entities;

namespace TokanPages.Persistence.DataAccess.Repositories.Messaging;

public interface IMessagingRepository
{
    Task<ServiceBusMessage?> GetServiceBusMessage(Guid messageId);

    Task<bool> CreateServiceBusMessage(Guid messageId);

    Task<bool> UpdateServiceBusMessage(Guid messageId, bool isConsumed);
}