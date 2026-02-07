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
    /// Returns list of use subscription by the given user ID.
    /// </summary>
    /// <param name="userId">A mandatory user ID.</param>
    /// <returns>If found, returns user subscription details, otherwise null.</returns>
    Task<UserSubscription?> GetUserSubscription(Guid userId);

    /// <summary>
    /// Creates new user subscription entry in the database.
    /// </summary>
    /// <param name="data">Subscription details.</param>
    Task CreateUserSubscription(CreateUserSubscriptionDto data);

    /// <summary>
    /// Updates existing user subscription entry in the database.
    /// </summary>
    /// <param name="data">Subscription details.</param>
    Task UpdateUserSubscription(UpdateUserSubscriptionDto data);

    /// <summary>
    /// Returns user payment details by the given user ID.
    /// </summary>
    /// <param name="userId">A mandatory user ID.</param>
    /// <returns>If found, returns user payment, otherwise null.</returns>
    Task<UserPayment?> GetUserPayment(Guid userId);

    /// <summary>
    /// Creates new user payment entry in the database.
    /// </summary>
    /// <param name="data">Payment details.</param>
    Task CreateUserPayment(CreateUserPaymentDto data);

    /// <summary>
    /// Updates existing user payment details entry in the database. 
    /// </summary>
    /// <param name="data">Payment details.</param>
    Task UpdateUserPayment(UpdateUserPaymentDto data);
}