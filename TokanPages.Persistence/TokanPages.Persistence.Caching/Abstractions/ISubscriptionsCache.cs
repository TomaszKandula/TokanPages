using TokanPages.Backend.Application.Revenue.Queries;

namespace TokanPages.Persistence.Caching.Abstractions;

/// <summary>
/// Subscription cache contract.
/// </summary>
public interface ISubscriptionsCache
{
    /// <summary>
    /// Returns current price list.
    /// </summary>
    /// <param name="languageIso">Obligatory language ISO (three letter code).</param>
    /// <param name="noCache">Enable/Disable REDIS cache.</param>
    /// <returns>Current price list.</returns>
    Task<GetSubscriptionPricesQueryResult> GetSubscriptionPrices(string languageIso, bool noCache = false);
}