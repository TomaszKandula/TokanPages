using TokanPages.Backend.Application.Newsletters.Queries;

namespace TokanPages.Persistence.Caching.Abstractions;

/// <summary>
/// Subscribers cache contract
/// </summary>
public interface INewslettersCache
{
    /// <summary>
    /// Returns newsletter subscribers
    /// </summary>
    /// <param name="noCache">Enable/disable REDIS cache</param>
    /// <returns>Object list</returns>
    Task<List<GetNewslettersQueryResult>> GetNewsletters(bool noCache = false);

    /// <summary>
    /// Returns single newsletter subscriber
    /// </summary>
    /// <param name="id">Subscriber ID</param>
    /// <param name="noCache">Enable/disable REDIS cache</param>
    /// <returns>Object</returns>
    Task<GetNewsletterQueryResult> GetNewsletter(Guid id, bool noCache = false);
}