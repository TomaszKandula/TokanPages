namespace TokanPages.Backend.Cqrs.Handlers.Commands.Subscribers
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Database;
    using Core.Exceptions;
    using Shared.Resources;
    using MediatR;

    public class RemoveSubscriberCommandHandler : TemplateHandler<RemoveSubscriberCommand, Unit>
    {
        private readonly DatabaseContext _databaseContext;

        public RemoveSubscriberCommandHandler(DatabaseContext databaseContext) 
            => _databaseContext = databaseContext;

        public override async Task<Unit> Handle(RemoveSubscriberCommand request, CancellationToken cancellationToken) 
        {
            var currentSubscriber = await _databaseContext.Subscribers
                .Where(subscribers => subscribers.Id == request.Id)
                .ToListAsync(cancellationToken);
            
            if (!currentSubscriber.Any())
                throw new BusinessException(nameof(ErrorCodes.SUBSCRIBER_DOES_NOT_EXISTS), ErrorCodes.SUBSCRIBER_DOES_NOT_EXISTS);

            _databaseContext.Subscribers.Remove(currentSubscriber.First());
            await _databaseContext.SaveChangesAsync(cancellationToken);
            return await Task.FromResult(Unit.Value);
       
        }
    }
}