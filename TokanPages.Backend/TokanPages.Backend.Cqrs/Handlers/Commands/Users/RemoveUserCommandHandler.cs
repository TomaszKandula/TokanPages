namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Database;
using Core.Exceptions;
using Shared.Resources;
using Core.Utilities.LoggerService;
using MediatR;

public class RemoveUserCommandHandler : Cqrs.RequestHandler<RemoveUserCommand, Unit>
{
    public RemoveUserCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService) : base(databaseContext, loggerService) { }

    public override async Task<Unit> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
    {
        var currentUser = await DatabaseContext.Users
            .Where(subscribers => subscribers.Id == request.Id)
            .ToListAsync(cancellationToken);

        if (!currentUser.Any())
            throw new BusinessException(nameof(ErrorCodes.USER_DOES_NOT_EXISTS), ErrorCodes.USER_DOES_NOT_EXISTS);

        DatabaseContext.Users.Remove(currentUser.First());
        await DatabaseContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}