namespace TokanPages.WebApi.Services.Caching.Subscribers;

using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.Backend.Cqrs.Handlers.Queries.Subscribers;

public interface ISubscribersCache
{
    Task<IEnumerable<GetAllSubscribersQueryResult>> GetSubscribers(bool noCache = false);

    Task<GetSubscriberQueryResult> GetSubscriber(Guid id, bool noCache = false);
}