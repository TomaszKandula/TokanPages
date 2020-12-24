using System;
using System.Threading;
using System.Threading.Tasks;
using TokanPages.Backend.Database;
using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Subscribers
{

    public class AddSubscriberCommandHandler : TemplateHandler<AddSubscriberCommand, Unit>
    {

        private readonly DatabaseContext FDatabaseContext;

        public AddSubscriberCommandHandler(DatabaseContext ADatabaseContext) 
        {
            FDatabaseContext = ADatabaseContext;
        }

        public override async Task<Unit> Handle(AddSubscriberCommand ARequest, CancellationToken ACancellationToken) 
        {

            var LNewSubscriber = new Domain.Entities.Subscribers
            {
                Email = ARequest.Email,
                Id = Guid.NewGuid(),
                Count = 0,
                IsActivated = true,
                LastUpdated = null,
                Registered = DateTime.UtcNow
            };

            await FDatabaseContext.Subscribers.AddAsync(LNewSubscriber);
            await FDatabaseContext.SaveChangesAsync(ACancellationToken);
            return await Task.FromResult(Unit.Value);
        
        }

    }

}
