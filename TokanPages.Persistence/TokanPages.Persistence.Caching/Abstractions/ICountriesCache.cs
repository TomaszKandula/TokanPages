using TokanPages.Backend.Application.Countries.Queries;

namespace TokanPages.Persistence.Caching.Abstractions;

/// <summary>
/// Countries cache contract.
/// </summary>
public interface ICountriesCache
{
    /// <summary>
    /// Returns country codes.
    /// </summary>
    /// <param name="filterBy">Optional filter.</param>
    /// <param name="noCache">Enable/disable REDIS cache.</param>
    /// <returns>List of codes.</returns>
    Task<IList<GetCountryCodesQueryResult>> GetCountryCodes(string filterBy, bool noCache = false);
}