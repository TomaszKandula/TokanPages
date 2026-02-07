using Microsoft.Extensions.Options;
using TokanPages.Backend.Configuration.Options;
using TokanPages.Backend.Domain.Entities;
using TokanPages.Backend.Domain.Entities.Users;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Persistence.DataAccess.Abstractions;

namespace TokanPages.Persistence.DataAccess.Repositories.Revenue;

public class RevenueRepository : RepositoryBase, IRevenueRepository
{
    public RevenueRepository(IDbOperations dbOperations, IOptions<AppSettingsModel> appSettings) 
        : base(dbOperations, appSettings) { }

    /// <inheritdoc/>
    public async Task<List<SubscriptionPricing>> GetSubscriptionPrices(string languageIso)
    {
        var filterBy = new { LanguageIso = languageIso };
        var data = await DbOperations.Retrieve<SubscriptionPricing>(filterBy);
        return data.ToList();
    }

    /// <inheritdoc/>
    public async Task<UserSubscription?> GetUserSubscription(Guid userId)
    {
        var filterBy = new { UserId = userId };
        var data = await DbOperations.Retrieve<UserSubscription>(filterBy);
        return data.SingleOrDefault();
    }
}