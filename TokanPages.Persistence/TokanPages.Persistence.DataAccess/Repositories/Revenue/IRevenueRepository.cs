using TokanPages.Backend.Domain.Entities;
using TokanPages.Backend.Domain.Entities.Users;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Persistence.DataAccess.Repositories.Revenue.Models;

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
    /// Returns available subscription price details based on provided arguments. 
    /// </summary>
    /// <param name="term">Payment term, i.e. '360' for one month, etc.</param>
    /// <param name="languageIso">Language ISO, i.e. 'POL'.</param>
    /// <param name="currencyIso">Currency ISO, i.e. 'PLN'.</param>
    /// <returns>Subscription price details.</returns>
    Task<SubscriptionPricing?> GetSubscriptionPrice(TermType term, string languageIso, string currencyIso);

    /// <summary>
    /// Returns list of use subscription.
    /// </summary>
    /// <param name="userId">A mandatory user ID.</param>
    /// <returns>If found, returns user subscription details, otherwise null.</returns>
    Task<UserSubscription?> GetUserSubscription(Guid userId);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    Task CreateUserSubscription(CreateUserSubscriptionDto data);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    Task UpdateUserSubscription(UpdateUserSubscriptionDto data);
}