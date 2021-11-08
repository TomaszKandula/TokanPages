namespace TokanPages.Backend.Cqrs.Handlers.Commands.Subscribers
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Database;
    using Core.Exceptions;
    using Shared.Resources;
    using Core.Utilities.LoggerService;
    using MediatR;

    public class RemoveSubscriberCommandHandler : TemplateHandler<RemoveSubscriberCommand, Unit>
    {
        public RemoveSubscriberCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService) : base(databaseContext, loggerService) { }

        public override async Task<Unit> Handle(RemoveSubscriberCommand request, CancellationToken cancellationToken) 
        {
            var currentSubscriber = await DatabaseContext.Subscribers
                .Where(subscribers => subscribers.Id == request.Id)
                .ToListAsync(cancellationToken);
            
            if (!currentSubscriber.Any())
                throw new BusinessException(nameof(ErrorCodes.SUBSCRIBER_DOES_NOT_EXISTS), ErrorCodes.SUBSCRIBER_DOES_NOT_EXISTS);

            DatabaseContext.Subscribers.Remove(currentSubscriber.First());
            await DatabaseContext.SaveChangesAsync(cancellationToken);
            return await Task.FromResult(Unit.Value);
        }
    }
}