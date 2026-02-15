using TokanPages.Backend.Domain.Entities;
using TokanPages.Backend.Domain.Entities.Users;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Persistence.DataAccess.Repositories.Revenue.Models;

namespace TokanPages.Persistence.DataAccess.Repositories.Revenue;

public interface IRevenueRepository
{
    Task <List<SubscriptionPricing>> GetSubscriptionPrices(string languageIso);

    Task<SubscriptionPricing?> GetSubscriptionPrice(TermType term, string languageIso, string currencyIso);

    Task<UserSubscription?> GetUserSubscription(Guid userId);

    Task CreateUserSubscription(CreateUserSubscriptionDto data);

    Task UpdateUserSubscription(UpdateUserSubscriptionDto data);

    Task RemoveUserSubscription(Guid userId);

    Task<UserPayment?> GetUserPayment(Guid userId);

    Task<UserPayment?> GetUserPayment(string extOrderId);

    Task CreateUserPayment(CreateUserPaymentDto data);

    Task UpdateUserPayment(UpdateUserPaymentDto data);

    Task CreateUserPaymentsHistory(CreateUserPaymentHistoryDto data);
}