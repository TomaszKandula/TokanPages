using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.Database;
using TokanPages.Services.UserService.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace TokanPages.Backend.Application.Subscriptions.Commands;

public class RemoveSubscriptionCommandHandler : RequestHandler<RemoveSubscriptionCommand, Unit>
{
    private readonly IUserService _userService;

    public RemoveSubscriptionCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IUserService userService) : base(databaseContext, loggerService) => _userService = userService;

    public override async Task<Unit> Handle(RemoveSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetActiveUser(request.UserId, cancellationToken: cancellationToken);
        var userSubscription = await DatabaseContext.UserSubscriptions
            .Where(subscriptions => subscriptions.UserId == user.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (userSubscription is null)
            throw new BusinessException(nameof(ErrorCodes.SUBSCRIPTION_DOES_NOT_EXISTS), ErrorCodes.SUBSCRIPTION_DOES_NOT_EXISTS);

        DatabaseContext.Remove(userSubscription);
        await DatabaseContext.SaveChangesAsync(cancellationToken);

        LoggerService.LogInformation($"Subscription for user ID '{user.Id}' has been removed.");
        return Unit.Value;
    }
}