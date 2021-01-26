using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Database;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Resources;
using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Subscribers
{
    public class RemoveSubscriberCommandHandler : TemplateHandler<RemoveSubscriberCommand, Unit>
    {
        private readonly DatabaseContext FDatabaseContext;

        public RemoveSubscriberCommandHandler(DatabaseContext ADatabaseContext) 
        {
            FDatabaseContext = ADatabaseContext;
        }

        public override async Task<Unit> Handle(RemoveSubscriberCommand ARequest, CancellationToken ACancellationToken) 
        {
            var LCurrentSubscriber = await FDatabaseContext.Subscribers
                .Where(ASubscribers => ASubscribers.Id == ARequest.Id)
                .ToListAsync(ACancellationToken);
            
            if (!LCurrentSubscriber.Any())
            {
                throw new BusinessException(nameof(ErrorCodes.SUBSCRIBER_DOES_NOT_EXISTS), ErrorCodes.SUBSCRIBER_DOES_NOT_EXISTS);
            }

            FDatabaseContext.Subscribers.Remove(LCurrentSubscriber.First());
            await FDatabaseContext.SaveChangesAsync(ACancellationToken);
            return await Task.FromResult(Unit.Value);
       
        }
    }
}
