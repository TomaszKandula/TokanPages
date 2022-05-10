namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Database;
using Core.Exceptions;
using Shared.Resources;
using Core.Utilities.LoggerService;
using Core.Utilities.DateTimeService;
using MediatR;

public class UpdateUserCommandHandler : Cqrs.RequestHandler<UpdateUserCommand, Unit>
{
    private readonly IDateTimeService _dateTimeService;
        
    public UpdateUserCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IDateTimeService dateTimeService) : base(databaseContext, loggerService)
    {
        _dateTimeService = dateTimeService;
    }

    public override async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var usersList = await DatabaseContext.Users
            .Where(users => users.Id == request.Id)
            .ToListAsync(cancellationToken);

        if (!usersList.Any())
            throw new BusinessException(nameof(ErrorCodes.USER_DOES_NOT_EXISTS), ErrorCodes.USER_DOES_NOT_EXISTS);

        var emailCollection = await DatabaseContext.Users
            .AsNoTracking()
            .Where(users => users.EmailAddress == request.EmailAddress)
            .ToListAsync(cancellationToken);

        if (emailCollection.Count == 1)
            throw new BusinessException(nameof(ErrorCodes.EMAIL_ADDRESS_ALREADY_EXISTS), ErrorCodes.EMAIL_ADDRESS_ALREADY_EXISTS);

        var currentUser = usersList.First();

        currentUser.IsActivated = request.IsActivated;
        currentUser.UserAlias = request.UserAlias ?? currentUser.UserAlias;
        currentUser.FirstName = request.FirstName ?? currentUser.FirstName;
        currentUser.LastName = request.LastName ?? currentUser.LastName;
        currentUser.EmailAddress = request.EmailAddress ?? currentUser.EmailAddress;
        currentUser.ShortBio = request.ShortBio ?? currentUser.ShortBio;
        currentUser.LastUpdated = _dateTimeService.Now;

        await DatabaseContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}