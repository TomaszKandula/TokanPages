using TokanPages.Backend.Application.Invoicing.Currencies.Queries;

namespace TokanPages.Persistence.Caching.Abstractions;

/// <summary>
/// Currencies cache contract.
/// </summary>
public interface ICurrenciesCache
{
    /// <summary>
    /// Returns currency codes.
    /// </summary>
    /// <param name="filterBy">Optional filter.</param>
    /// <param name="noCache">Enable/disable REDIS cache.</param>
    /// <returns>List of codes.</returns>
    Task<IList<GetCurrencyCodesQueryResult>> GetCurrencyCodes(string filterBy, bool noCache = false);
}