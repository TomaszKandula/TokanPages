using System;
using System.Threading;
using System.Threading.Tasks;
using TokanPages.Backend.Database;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Resources;
using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Subscribers
{
    
    public class UpdateSubscriberCommandHandler : TemplateHandler<UpdateSubscriberCommand, Unit>
    {

        private readonly DatabaseContext FDatabaseContext;

        public UpdateSubscriberCommandHandler(DatabaseContext ADatabaseContext) 
        {
            FDatabaseContext = ADatabaseContext;
        }

        public override async Task<Unit> Handle(UpdateSubscriberCommand ARequest, CancellationToken ACancellationToken) 
        {

            var LCurrentSubscriber = await FDatabaseContext.Subscribers.FindAsync(new object[] { ARequest .Id}, ACancellationToken);
            if (LCurrentSubscriber == null) 
            {
                throw new BusinessException(nameof(ErrorCodes.SUBSCRIBER_DOES_NOT_EXISTS), ErrorCodes.SUBSCRIBER_DOES_NOT_EXISTS);
            }

            LCurrentSubscriber.Email = ARequest.Email;
            LCurrentSubscriber.Count = ARequest.Count;
            LCurrentSubscriber.IsActivated = ARequest.IsActivated;
            LCurrentSubscriber.LastUpdated = DateTime.UtcNow;

            await FDatabaseContext.SaveChangesAsync(ACancellationToken);
            return await Task.FromResult(Unit.Value);

        }

    }

}
