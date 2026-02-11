using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Services.UserService.Abstractions;
using MediatR;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Persistence.DataAccess.Repositories.Revenue;
using TokanPages.Persistence.DataAccess.Repositories.Revenue.Models;

namespace TokanPages.Backend.Application.Revenue.Commands;

public class UpdateSubscriptionCommandHandler : RequestHandler<UpdateSubscriptionCommand, Unit>
{
    private readonly IUserService _userService;

    private readonly IRevenueRepository _revenueRepository;

    public UpdateSubscriptionCommandHandler(OperationDbContext operationDbContext, ILoggerService loggerService, 
        IUserService userService, IRevenueRepository revenueRepository) : base(operationDbContext, loggerService)
    {
        _userService = userService;
        _revenueRepository = revenueRepository;
    }

    public override async Task<Unit> Handle(UpdateSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetActiveUser(request.UserId);

        var userSubscription = await _revenueRepository.GetUserSubscription(user.UserId);
        if (userSubscription is null)
            throw new BusinessException(nameof(ErrorCodes.SUBSCRIPTION_DOES_NOT_EXISTS), ErrorCodes.SUBSCRIPTION_DOES_NOT_EXISTS);

        var updateBy = new UpdateUserSubscriptionDto
        {
            AutoRenewal = request.AutoRenewal ?? userSubscription.AutoRenewal,
            Term = request.Term ?? userSubscription.Term,
            TotalAmount = request.TotalAmount ?? userSubscription.TotalAmount,
            CurrencyIso = request.CurrencyIso ?? userSubscription.CurrencyIso,
            ExtCustomerId =  userSubscription.ExtCustomerId,
            ExtOrderId = request.ExtOrderId ?? userSubscription.ExtOrderId,
            ModifiedBy = user.UserId,
            IsActive =  userSubscription.IsActive,
            CompletedAt = userSubscription.CompletedAt,
            ExpiresAt = userSubscription.ExpiresAt
        };

        await _revenueRepository.UpdateUserSubscription(updateBy);

        LoggerService.LogInformation("Existing subscription has been updated.");
        return Unit.Value;
    }
}