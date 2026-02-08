using Microsoft.Extensions.Options;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Domain.Entities;
using TokanPages.Backend.Domain.Entities.Users;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Backend.Shared.Options;
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
            CreatedBy = data.UserId,
            IsActive = false
        };

        await DbOperations.Insert(entity);
    }

    /// <inheritdoc/>
    public async Task UpdateUserSubscription(UpdateUserSubscriptionDto data)
    {
        var updateBy = new
        {
            AutoRenewal = data.AutoRenewal,
            Term = data.Term,
            TotalAmount = data.TotalAmount,
            CurrencyIso = data.CurrencyIso,
            ExtCustomerId = data.ExtCustomerId,
            ExtOrderId = data.ExtOrderId,
            ModifiedAt = _dateTimeService.Now,
            ModifiedBy = data.ModifiedBy,
            IsActive = data.IsActive,
            CompletedAt = data.CompletedAt,
            ExpiresAt = data.ExpiresAt
        };

        var filterBy = new
        {
            UserId = data.ModifiedBy
        };

        await DbOperations.Update<UserSubscription>(updateBy, filterBy);
    }

    /// <inheritdoc/>
    public async Task RemoveUserSubscription(Guid userId)
    {
        var deleteBy = new { UserId = userId };
        await DbOperations.Delete<UserSubscription>(deleteBy);
    }

    /// <inheritdoc/>
    public async Task<UserPayment?> GetUserPayment(Guid userId)
    {
        var filterBy = new { UserId = userId };
        var data = await DbOperations.Retrieve<UserPayment>(filterBy);
        return data.SingleOrDefault();
    }

    /// <inheritdoc/>
    public async Task<UserPayment?> GetUserPayment(string extOrderId)
    {
        var filterBy = new { ExtOrderId = extOrderId };
        var data = await DbOperations.Retrieve<UserPayment>(filterBy);
        return data.SingleOrDefault();
    }

    /// <inheritdoc/>
    public async Task CreateUserPayment(CreateUserPaymentDto data)
    {
        var entity = new UserPayment
        {
            Id = data.Id ?? Guid.NewGuid(),
            UserId = data.UserId,
            PmtOrderId = data.PmtOrderId,
            PmtStatus = data.PmtStatus,
            PmtType = data.PmtType,
            PmtToken = data.PmtToken,
            CreatedAt = _dateTimeService.Now,
            CreatedBy = data.UserId,
            ExtOrderId = data.ExtOrderId
        };

        await DbOperations.Insert(entity);
    }

    /// <inheritdoc/>
    public async Task UpdateUserPayment(UpdateUserPaymentDto data)
    {
        var updateBy = new
        {
            PmtOrderId = data.PmtOrderId,
            PmtStatus = data.PmtStatus,
            PmtType = data.PmtType,
            PmtToken = data.PmtToken,
            ModifiedAt = _dateTimeService.Now,
            ModifiedBy = data.ModifiedBy,
            CreatedAt = data.CreatedAt,
            CreatedBy = data.CreatedBy,
            ExtOrderId = data.ExtOrderId
        };

        var filterBy = new
        {
            UserId = data.ModifiedBy
        };

        await DbOperations.Update<UserPayment>(updateBy, filterBy);
    }

    /// <inheritdoc/>
    public async Task CreateUserPaymentsHistory(CreateUserPaymentHistoryDto data)
    {
        var entity = new UserPaymentHistory
        {
            Id = Guid.NewGuid(),
            UserId = data.UserId,
            Amount = data.Amount,
            CurrencyIso = data.CurrencyIso,
            Term = data.Term,
            CreatedAt = _dateTimeService.Now,
            CreatedBy = data.CreatedBy,
        };

        await DbOperations.Insert(entity);
    }
}