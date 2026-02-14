using TokanPages.Backend.Domain.Entities;

namespace TokanPages.Persistence.DataAccess.Repositories.Messaging;

public interface IMessagingRepository
{
    Task<ServiceBusMessage?> GetServiceBusMessage(Guid messageId);

    Task CreateServiceBusMessage(Guid messageId);

    Task UpdateServiceBusMessage(Guid messageId, bool isConsumed);

    Task DeleteServiceBusMessage(Guid messageId);
}