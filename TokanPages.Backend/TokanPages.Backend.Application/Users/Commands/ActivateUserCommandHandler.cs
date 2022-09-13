using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.Database;

namespace TokanPages.Backend.Application.Users.Commands;

public class ActivateUserCommandHandler : RequestHandler<ActivateUserCommand, Unit>
{
    private readonly IDateTimeService _dateTimeService;

    public ActivateUserCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IDateTimeService dateTimeService) : base(databaseContext, loggerService) => _dateTimeService = dateTimeService;

    public override async Task<Unit> Handle(ActivateUserCommand request, CancellationToken cancellationToken)
    {
        var users = await DatabaseContext.Users
            .SingleOrDefaultAsync(users => users.ActivationId == request.ActivationId, cancellationToken);

        if (users == null)
            throw new BusinessException(nameof(ErrorCodes.INVALID_ACTIVATION_ID), ErrorCodes.INVALID_ACTIVATION_ID);

        if (users.ActivationIdEnds < _dateTimeService.Now)
            throw new BusinessException(nameof(ErrorCodes.EXPIRED_ACTIVATION_ID), ErrorCodes.EXPIRED_ACTIVATION_ID);
            
        users.IsActivated = true;
        users.ActivationId = null;
        users.ActivationIdEnds = null;

        LoggerService.LogInformation($"User account has been activated, user ID: {users.Id}");
        await DatabaseContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}