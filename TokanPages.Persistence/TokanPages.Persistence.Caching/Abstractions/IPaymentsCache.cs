using TokanPages.Backend.Application.Invoicing.Payments.Queries;

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
    Task<IList<GetPaymentStatusQueryResult>> GetPaymentStatus(string filterBy, bool noCache = false);

    /// <summary>
    /// Returns payment type codes.
    /// </summary>
    /// <param name="filterBy">Optional filter.</param>
    /// <param name="noCache">Enable/disable REDIS cache.</param>
    /// <returns>List of codes.</returns>
    Task<IList<GetPaymentTypeQueryResult>> GetPaymentType(string filterBy, bool noCache = false);
}