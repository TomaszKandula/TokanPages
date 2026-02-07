using TokanPages.Backend.Domain.Entities;
using TokanPages.Backend.Domain.Entities.Users;

namespace TokanPages.Persistence.DataAccess.Repositories.Revenue;

public interface IRevenueRepository
{
    /// <summary>
    /// Returns list of subscription prices by the given language ISO.
    /// </summary>
    /// <param name="languageIso">User language.</param>
    /// <returns>List of available subscription prices.</returns>
    Task <List<SubscriptionPricing>> GetSubscriptionPrices(string languageIso);

    /// <summary>
    /// Returns list of use subscription.
    /// </summary>
    /// <param name="userId">A mandatory user ID.</param>
    /// <returns>If found, returns user subscription details, otherwise null.</returns>
    Task<UserSubscription?> GetUserSubscription(Guid userId);
}