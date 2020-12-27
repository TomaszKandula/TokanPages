using System;
using System.Threading;
using System.Threading.Tasks;
using TokanPages.Backend.Database;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Subscribers
{

    public class AddSubscriberCommandHandler : TemplateHandler<AddSubscriberCommand, Guid>
    {

        private readonly DatabaseContext FDatabaseContext;

        public AddSubscriberCommandHandler(DatabaseContext ADatabaseContext) 
        {
            FDatabaseContext = ADatabaseContext;
        }

        public override async Task<Guid> Handle(AddSubscriberCommand ARequest, CancellationToken ACancellationToken) 
        {

            var LNewId = Guid.NewGuid();
            var LNewSubscriber = new Domain.Entities.Subscribers
            {
                Email = ARequest.Email,
                Id = LNewId,
                Count = 0,
                IsActivated = true,
                LastUpdated = null,
                Registered = DateTime.UtcNow
            };

            await FDatabaseContext.Subscribers.AddAsync(LNewSubscriber);
            await FDatabaseContext.SaveChangesAsync(ACancellationToken);
            return await Task.FromResult(LNewId);
        
        }

    }

}
