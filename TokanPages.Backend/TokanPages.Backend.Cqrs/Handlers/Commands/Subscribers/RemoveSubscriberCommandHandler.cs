namespace TokanPages.Backend.Cqrs.Handlers.Commands.Subscribers;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Database;
using Core.Exceptions;
using Shared.Resources;
using Core.Utilities.LoggerService;
using MediatR;

public class RemoveSubscriberCommandHandler : Cqrs.RequestHandler<RemoveSubscriberCommand, Unit>
{
    public RemoveSubscriberCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService) 
        : base(databaseContext, loggerService) { }

    public override async Task<Unit> Handle(RemoveSubscriberCommand request, CancellationToken cancellationToken) 
    {
        var subscriber = await DatabaseContext.Subscribers
            .Where(subscribers => subscribers.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);
            
        if (subscriber is null)
            throw new BusinessException(nameof(ErrorCodes.SUBSCRIBER_DOES_NOT_EXISTS), ErrorCodes.SUBSCRIBER_DOES_NOT_EXISTS);

        DatabaseContext.Subscribers.Remove(subscriber);
        await DatabaseContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}