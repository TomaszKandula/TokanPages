using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Entities.User;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.Database;
using TokanPages.Services.UserService.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace TokanPages.Backend.Application.Subscriptions.Commands;

public class AddSubscriptionCommandHandler : RequestHandler<AddSubscriptionCommand, AddSubscriptionCommandResult>
{
    private readonly IDateTimeService _dateTimeService;

    private readonly IUserService _userService;

    public AddSubscriptionCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IDateTimeService dateTimeService, IUserService userService) : base(databaseContext, loggerService)
    {
        _dateTimeService = dateTimeService;
        _userService = userService;
    }

    public override async Task<AddSubscriptionCommandResult> Handle(AddSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var currency = request.UserCurrency ?? "pln";
        var language = request.UserLanguage ?? "pol";

        var user = await _userService.GetActiveUser(request.UserId, cancellationToken: cancellationToken);
        var price = await DatabaseContext.SubscriptionPricing
            .Where(pricing => pricing.Term == request.SelectedTerm)
            .Where(pricing => pricing.CurrencyIso == currency.ToUpper())
            .Where(pricing => pricing.LanguageIso == language.ToUpper())
            .SingleOrDefaultAsync(cancellationToken);

        if (price is null)
            throw new BusinessException(nameof(ErrorCodes.PRICE_NOT_FOUND), ErrorCodes.PRICE_NOT_FOUND);

        var userSubscription = await DatabaseContext.UserSubscriptions
            .Where(subscriptions => subscriptions.UserId == user.Id)
            .SingleOrDefaultAsync(cancellationToken);

        var extCustomerId = Guid.NewGuid().ToString("N");
        var extOrderId = Guid.NewGuid().ToString("N");
        var userInfo = await DatabaseContext.UserInfo
            .AsNoTracking()
            .Where(info => info.UserId == user.Id)
            .Select(info => new
            {
                info.FirstName,
                info.LastName
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (userInfo is null)
            throw new BusinessException(nameof(ErrorCodes.USER_DOES_NOT_EXISTS), ErrorCodes.USER_DOES_NOT_EXISTS);

        AddSubscriptionCommandResult result;
        if (userSubscription is not null)
        {
            userSubscription.AutoRenewal = true;
            userSubscription.Term = request.SelectedTerm;
            userSubscription.TotalAmount = price.Price;
            userSubscription.CurrencyIso = price.CurrencyIso;
            userSubscription.ExtCustomerId = extCustomerId;
            userSubscription.ExtOrderId = extOrderId;
            userSubscription.ModifiedAt = _dateTimeService.Now;
            userSubscription.ModifiedBy = user.Id;

            result = new AddSubscriptionCommandResult
            {
                UserId = user.Id,
                SubscriptionId = userSubscription.Id,
                ExtOrderId = extOrderId,
                Email = user.EmailAddress,
                FirstName = userInfo.FirstName,
                LastName = userInfo.LastName
            };
        }
        else
        {
            var subscription = new UserSubscription
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                AutoRenewal = true,
                Term = request.SelectedTerm,
                TotalAmount = price.Price,
                CurrencyIso = price.CurrencyIso,
                ExtCustomerId = extCustomerId,
                ExtOrderId = extOrderId,
                CreatedAt = _dateTimeService.Now,
                CreatedBy = user.Id
            };

            result = new AddSubscriptionCommandResult
            {
                UserId = subscription.UserId,
                SubscriptionId = subscription.Id,
                ExtOrderId = subscription.ExtOrderId,
                Email = user.EmailAddress,
                FirstName = userInfo.FirstName,
                LastName = userInfo.LastName
            };

            await DatabaseContext.UserSubscriptions.AddAsync(subscription, cancellationToken);
        }

        await DatabaseContext.SaveChangesAsync(cancellationToken);
        LoggerService.LogInformation("New user subscription has been registered.");

        return result;
    }
}