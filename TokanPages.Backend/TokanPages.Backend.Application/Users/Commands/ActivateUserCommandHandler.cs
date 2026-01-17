using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Persistence.Database;

namespace TokanPages.Backend.Application.Users.Commands;

public class ActivateUserCommandHandler : RequestHandler<ActivateUserCommand, ActivateUserCommandResult>
{
    private readonly IDateTimeService _dateTimeService;

    public ActivateUserCommandHandler(OperationDbContext operationDbContext, ILoggerService loggerService, 
        IDateTimeService dateTimeService) : base(operationDbContext, loggerService) => _dateTimeService = dateTimeService;

    public override async Task<ActivateUserCommandResult> Handle(ActivateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await OperationDbContext.Users
            .Where(users => users.ActivationId == request.ActivationId)
            .Where(users => !users.IsDeleted)
            .SingleOrDefaultAsync(cancellationToken);

        if (user is null)
            throw new BusinessException(nameof(ErrorCodes.INVALID_ACTIVATION_ID), ErrorCodes.INVALID_ACTIVATION_ID);

        if (user.ActivationIdEnds < _dateTimeService.Now)
            throw new BusinessException(nameof(ErrorCodes.EXPIRED_ACTIVATION_ID), ErrorCodes.EXPIRED_ACTIVATION_ID);

        user.IsActivated = true;
        user.IsVerified = true;
        user.ActivationId = null;
        user.ActivationIdEnds = null;

        if (user.HasBusinessLock)
            LoggerService.LogWarning("The user is activated but has the business lock and thus will require administrator action.");

        LoggerService.LogInformation($"User account has been activated, user ID: {user.Id}");
        await OperationDbContext.SaveChangesAsync(cancellationToken);

        return new ActivateUserCommandResult
        {
            UserId = user.Id,
            HasBusinessLock = user.HasBusinessLock
        };
    }
}