namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users;

using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Database;
using Core.Exceptions;
using Shared.Resources;
using Core.Utilities.LoggerService;
using Core.Utilities.DateTimeService;
    
public class ActivateUserCommandHandler : Cqrs.RequestHandler<ActivateUserCommand, Unit>
{
    private readonly IDateTimeService _dateTimeService;
       
    public ActivateUserCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IDateTimeService dateTimeService) : base(databaseContext, loggerService)
    {
        _dateTimeService = dateTimeService;
    }

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