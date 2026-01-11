using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;
using TokanPages.Services.UserService.Abstractions;
using Microsoft.EntityFrameworkCore;
using TokanPages.Persistence.Database.Contexts;

namespace TokanPages.Backend.Application.Revenue.Queries;

public class GetSubscriptionQueryHandler : RequestHandler<GetSubscriptionQuery, GetSubscriptionQueryResult>
{
    private readonly IUserService _userService;

    public GetSubscriptionQueryHandler(DatabaseContext databaseContext, ILoggerService loggerService, IUserService userService) 
        : base(databaseContext, loggerService) => _userService = userService;

    public override async Task<GetSubscriptionQueryResult> Handle(GetSubscriptionQuery request, CancellationToken cancellationToken)
    {
        var userId = _userService.GetLoggedUserId();
        var query = await DatabaseContext.UserSubscriptions
            .AsNoTracking()
            .Where(subscriptions => subscriptions.UserId == userId)
            .Select(subscriptions => new GetSubscriptionQueryResult
            {
                UserId = subscriptions.UserId,
                IsActive = subscriptions.IsActive,
                AutoRenewal = subscriptions.AutoRenewal,
                Term = subscriptions.Term,
                TotalAmount = subscriptions.TotalAmount,
                CurrencyIso = subscriptions.CurrencyIso,
                ExtCustomerId = subscriptions.ExtCustomerId,
                ExtOrderId = subscriptions.ExtOrderId,
                CreatedBy = subscriptions.CreatedBy,
                CreatedAt = subscriptions.CreatedAt,
                ModifiedBy = subscriptions.ModifiedBy,
                ModifiedAt = subscriptions.ModifiedAt,
            })
            .SingleOrDefaultAsync(cancellationToken);

        return query ?? new GetSubscriptionQueryResult();
    }
}