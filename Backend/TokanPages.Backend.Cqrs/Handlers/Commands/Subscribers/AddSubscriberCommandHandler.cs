using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Database;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Resources;

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

            var LEmailCollection = await FDatabaseContext.Subscribers
                .AsNoTracking()
                .Where(AUsers => AUsers.Email == ARequest.Email)
                .ToListAsync(ACancellationToken);

            if (LEmailCollection.Count > 1)
            {
                throw new BusinessException(nameof(ErrorCodes.EMAIL_ADDRESS_ALREADY_EXISTS), ErrorCodes.EMAIL_ADDRESS_ALREADY_EXISTS);
            }

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
