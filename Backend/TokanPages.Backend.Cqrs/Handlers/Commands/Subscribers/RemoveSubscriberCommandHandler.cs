using System.Threading;
using System.Threading.Tasks;
using TokanPages.Backend.Database;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Resources;
using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Subscribers
{

    public class RemoveSubscriberCommandHandler : IRequestHandler<RemoveSubscriberCommand, Unit>
    {

        private readonly DatabaseContext FDatabaseContext;

        public RemoveSubscriberCommandHandler(DatabaseContext ADatabaseContext) 
        {
            FDatabaseContext = ADatabaseContext;
        }

        public async Task<Unit> Handle(RemoveSubscriberCommand ARequest, CancellationToken ACancellationToken) 
        {

            var LCurrentSubscriber = await FDatabaseContext.Subscribers.FindAsync(new object[] { ARequest.Id }, ACancellationToken);
            if (LCurrentSubscriber == null) 
            {
                throw new BusinessException(nameof(ErrorCodes.SUBSCRIBER_DOES_NOT_EXISTS), ErrorCodes.SUBSCRIBER_DOES_NOT_EXISTS);
            }

            FDatabaseContext.Subscribers.Remove(LCurrentSubscriber);
            await FDatabaseContext.SaveChangesAsync();

            return await Task.FromResult(Unit.Value);
        
        }

    }

}
