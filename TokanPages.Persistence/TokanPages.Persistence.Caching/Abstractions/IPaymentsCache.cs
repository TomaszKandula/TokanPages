using TokanPages.Backend.Application.Payments.Queries;

namespace TokanPages.Persistence.Caching.Abstractions;

/// <summary>
/// Payments cache contract.
/// </summary>
public interface IPaymentsCache
{
    /// <summary>
    /// Returns payment status codes.
    /// </summary>
    /// <param name="filterBy">Optional filter.</param>
    /// <param name="noCache">Enable/disable REDIS cache.</param>
    /// <returns>List of codes.</returns>
    Task<IList<GetPaymentStatusListQueryResult>> GetPaymentStatusList(string filterBy, bool noCache = false);

    /// <summary>
    /// Returns payment type codes.
    /// </summary>
    /// <param name="filterBy">Optional filter.</param>
    /// <param name="noCache">Enable/disable REDIS cache.</param>
    /// <returns>List of codes.</returns>
    Task<IList<GetPaymentTypeListQueryResult>> GetPaymentTypeList(string filterBy, bool noCache = false);
}