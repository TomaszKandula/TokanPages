using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Services.UserService.Abstractions;
using MediatR;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Repositories.Revenue;

namespace TokanPages.Backend.Application.Revenue.Commands;

public class RemoveSubscriptionCommandHandler : RequestHandler<RemoveSubscriptionCommand, Unit>
{
    private readonly IUserService _userService;

    private readonly IRevenueRepository _revenueRepository;

    public RemoveSubscriptionCommandHandler(ILoggerService loggerService, 
        IUserService userService, IRevenueRepository revenueRepository) : base(loggerService)
    {
        _userService = userService;
        _revenueRepository = revenueRepository;
    }

    public override async Task<Unit> Handle(RemoveSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetActiveUser(request.UserId);
        var userSubscription = await _revenueRepository.GetUserSubscription(user.UserId);
        if (userSubscription is null)
            throw new BusinessException(nameof(ErrorCodes.SUBSCRIPTION_DOES_NOT_EXISTS), ErrorCodes.SUBSCRIPTION_DOES_NOT_EXISTS);

        await _revenueRepository.RemoveUserSubscription(user.UserId);
        LoggerService.LogInformation($"Subscription for user ID '{user.UserId}' has been removed.");

        return Unit.Value;
    }
}