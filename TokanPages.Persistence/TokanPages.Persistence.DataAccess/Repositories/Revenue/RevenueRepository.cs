using Microsoft.Extensions.Options;
using TokanPages.Backend.Configuration.Options;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Domain.Entities;
using TokanPages.Backend.Domain.Entities.Users;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Persistence.DataAccess.Abstractions;
using TokanPages.Persistence.DataAccess.Repositories.Revenue.Models;

namespace TokanPages.Persistence.DataAccess.Repositories.Revenue;

public class RevenueRepository : RepositoryBase, IRevenueRepository
{
    private readonly IDateTimeService _dateTimeService;

    public RevenueRepository(IDbOperations dbOperations, IOptions<AppSettingsModel> appSettings, IDateTimeService dateTimeService) 
        : base(dbOperations, appSettings) => _dateTimeService = dateTimeService;

    /// <inheritdoc/>
    public async Task<List<SubscriptionPricing>> GetSubscriptionPrices(string languageIso)
    {
        var filterBy = new { LanguageIso = languageIso };
        var data = await DbOperations.Retrieve<SubscriptionPricing>(filterBy);
        return data.ToList();
    }

    /// <inheritdoc/>
    public async Task<SubscriptionPricing?> GetSubscriptionPrice(TermType term, string languageIso, string currencyIso)
    {
        var filterBy = new
        {
            Term = term,
            CurrencyIso = currencyIso,
            LanguageIso = languageIso
        };

        var prices = await DbOperations.Retrieve<SubscriptionPricing>(filterBy);
        return prices.SingleOrDefault();
    }

    /// <inheritdoc/>
    public async Task<UserSubscription?> GetUserSubscription(Guid userId)
    {
        var filterBy = new { UserId = userId };
        var data = await DbOperations.Retrieve<UserSubscription>(filterBy);
        return data.SingleOrDefault();
    }

    /// <inheritdoc/>
    public async Task CreateUserSubscription(CreateUserSubscriptionDto data)
    {
        var entity = new UserSubscription
        {
            Id = data.Id ?? Guid.NewGuid(),
            UserId = data.UserId,
            AutoRenewal = true,
            Term = data.Term,
            TotalAmount =  data.TotalAmount,
            CurrencyIso = data.CurrencyIso,
            ExtCustomerId = data.ExtCustomerId,
            ExtOrderId =  data.ExtOrderId,
            CreatedAt = _dateTimeService.Now,
        };

        await DbOperations.Insert(entity);
    }

    /// <inheritdoc/>
    public async Task UpdateUserSubscription(UpdateUserSubscriptionDto data)
    {
        var updateBy = new
        {
            AutoRenewal = true,
            Term = data.Term,
            TotalAmount = data.TotalAmount,
            CurrencyIso = data.CurrencyIso,
            ExtCustomerId = data.ExtCustomerId,
            extOrderId = data.ExtOrderId,
            ModifiedAt = _dateTimeService.Now,
            ModifiedBy = data.ModifiedBy
        };

        var filterBy = new
        {
            UserId = data.ModifiedBy
        };

        await DbOperations.Update<UserSubscription>(updateBy, filterBy);
    }
}