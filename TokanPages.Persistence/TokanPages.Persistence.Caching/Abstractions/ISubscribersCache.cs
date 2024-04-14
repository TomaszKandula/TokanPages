using TokanPages.Backend.Application.Subscribers.Queries;

namespace TokanPages.Persistence.Caching.Abstractions;

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
    Task<List<GetAllNewslettersQueryResult>> GetSubscribers(bool noCache = false);

    /// <summary>
    /// Returns single newsletter subscriber
    /// </summary>
    /// <param name="id">Subscriber ID</param>
    /// <param name="noCache">Enable/disable REDIS cache</param>
    /// <returns>Object</returns>
    Task<GetNewsletterQueryResult> GetSubscriber(Guid id, bool noCache = false);
}