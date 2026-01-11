using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.Database;
using TokanPages.Services.UserService.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TokanPages.Persistence.Database.Contexts;

namespace TokanPages.Backend.Application.Revenue.Commands;

public class RemoveSubscriptionCommandHandler : RequestHandler<RemoveSubscriptionCommand, Unit>
{
    private readonly IUserService _userService;

    public RemoveSubscriptionCommandHandler(OperationsDbContext operationsDbContext, ILoggerService loggerService, 
        IUserService userService) : base(operationsDbContext, loggerService) => _userService = userService;

    public override async Task<Unit> Handle(RemoveSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetActiveUser(request.UserId, cancellationToken: cancellationToken);
        var userSubscription = await OperationsDbContext.UserSubscriptions
            .Where(subscriptions => subscriptions.UserId == user.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (userSubscription is null)
            throw new BusinessException(nameof(ErrorCodes.SUBSCRIPTION_DOES_NOT_EXISTS), ErrorCodes.SUBSCRIPTION_DOES_NOT_EXISTS);

        OperationsDbContext.Remove(userSubscription);
        await OperationsDbContext.SaveChangesAsync(cancellationToken);

        LoggerService.LogInformation($"Subscription for user ID '{user.Id}' has been removed.");
        return Unit.Value;
    }
}