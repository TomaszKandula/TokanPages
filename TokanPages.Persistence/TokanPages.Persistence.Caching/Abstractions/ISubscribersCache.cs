namespace TokanPages.Persistence.Caching.Abstractions;

using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.Backend.Cqrs.Handlers.Queries.Subscribers;

/// <summary>
/// Subscribers cache contract
/// </summary>
public interface ISubscribersCache
{
    /// <summary>
    /// Returns newsletter subscribers
    /// </summary>
    /// <param name="noCache">Enable/disable REDIS cache</param>
    /// <returns>Object list</returns>
    Task<List<GetAllSubscribersQueryResult>> GetSubscribers(bool noCache = false);

    /// <summary>
    /// Returns single newsletter subscriber
    /// </summary>
    /// <param name="id">Subscriber ID</param>
    /// <param name="noCache">Enable/disable REDIS cache</param>
    /// <returns>Object</returns>
    Task<GetSubscriberQueryResult> GetSubscriber(Guid id, bool noCache = false);
}