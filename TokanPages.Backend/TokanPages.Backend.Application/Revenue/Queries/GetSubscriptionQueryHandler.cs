using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Services.UserService.Abstractions;
using TokanPages.Persistence.DataAccess.Repositories.Revenue;

namespace TokanPages.Backend.Application.Revenue.Queries;

public class GetSubscriptionQueryHandler : RequestHandler<GetSubscriptionQuery, GetSubscriptionQueryResult>
{
    private readonly IUserService _userService;

    private readonly IRevenueRepository _revenueRepository;

    public GetSubscriptionQueryHandler(ILoggerService loggerService, IUserService userService, 
        IRevenueRepository revenueRepository) : base(loggerService)
    {
        _userService = userService;
        _revenueRepository = revenueRepository;
    }

    public override async Task<GetSubscriptionQueryResult> Handle(GetSubscriptionQuery request, CancellationToken cancellationToken)
    {
        var userId = _userService.GetLoggedUserId();
        var subscription = await _revenueRepository.GetUserSubscription(userId);
        if (subscription == null)
            return new GetSubscriptionQueryResult();

        var result = new GetSubscriptionQueryResult
        {
            UserId = subscription.UserId,
            IsActive = subscription.IsActive,
            AutoRenewal = subscription.AutoRenewal,
            Term = subscription.Term,
            TotalAmount = subscription.TotalAmount,
            CurrencyIso = subscription.CurrencyIso,
            ExtCustomerId = subscription.ExtCustomerId,
            ExtOrderId = subscription.ExtOrderId,
            CreatedBy = subscription.CreatedBy,
            CreatedAt = subscription.CreatedAt,
            ModifiedBy = subscription.ModifiedBy,
            ModifiedAt = subscription.ModifiedAt,
        };

        return result;
    }
}