using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.Database;
using TokanPages.Services.UserService.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace TokanPages.Backend.Application.Revenue.Commands;

public class UpdateSubscriptionCommandHandler : RequestHandler<UpdateSubscriptionCommand, Unit>
{
    private readonly IUserService _userService;

    private readonly IDateTimeService _dateTimeService;

    public UpdateSubscriptionCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IUserService userService, IDateTimeService dateTimeService) : base(databaseContext, loggerService)
    {
        _userService = userService;
        _dateTimeService = dateTimeService;
    }

    public override async Task<Unit> Handle(UpdateSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetActiveUser(request.UserId, cancellationToken: cancellationToken);
        var userSubscription = await DatabaseContext.UserSubscriptions
            .Where(subscriptions => subscriptions.UserId == user.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (userSubscription is null)
            throw new BusinessException(nameof(ErrorCodes.SUBSCRIPTION_DOES_NOT_EXISTS), ErrorCodes.SUBSCRIPTION_DOES_NOT_EXISTS);

        userSubscription.ExtOrderId = request.ExtOrderId ?? userSubscription.ExtOrderId;
        userSubscription.AutoRenewal = request.AutoRenewal ?? userSubscription.AutoRenewal;
        userSubscription.Term = request.Term ?? userSubscription.Term;
        userSubscription.TotalAmount = request.TotalAmount ?? userSubscription.TotalAmount;
        userSubscription.CurrencyIso = request.CurrencyIso ?? userSubscription.CurrencyIso;
        userSubscription.ModifiedBy = user.Id;
        userSubscription.ModifiedAt = _dateTimeService.Now;

        await DatabaseContext.SaveChangesAsync(cancellationToken);
        LoggerService.LogInformation("Existing subscription has been updated.");
        return Unit.Value;
    }
}