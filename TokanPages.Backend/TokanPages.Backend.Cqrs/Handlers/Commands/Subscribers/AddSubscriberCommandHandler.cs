namespace TokanPages.Backend.Cqrs.Handlers.Commands.Subscribers
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Database;
    using Core.Exceptions;
    using Shared.Resources;
    using Shared.Services.DateTimeService;

    public class AddSubscriberCommandHandler : TemplateHandler<AddSubscriberCommand, Guid>
    {
        private readonly DatabaseContext FDatabaseContext;
        
        private readonly IDateTimeService FDateTimeService;
        
        public AddSubscriberCommandHandler(DatabaseContext ADatabaseContext, IDateTimeService ADateTimeService) 
        {
            FDatabaseContext = ADatabaseContext;
            FDateTimeService = ADateTimeService;
        }

        public override async Task<Guid> Handle(AddSubscriberCommand ARequest, CancellationToken ACancellationToken) 
        {
            var LEmailCollection = await FDatabaseContext.Subscribers
                .AsNoTracking()
                .Where(AUsers => AUsers.Email == ARequest.Email)
                .ToListAsync(ACancellationToken);

            if (LEmailCollection.Count == 1)
                throw new BusinessException(nameof(ErrorCodes.EMAIL_ADDRESS_ALREADY_EXISTS), ErrorCodes.EMAIL_ADDRESS_ALREADY_EXISTS);

            var LNewSubscriber = new Domain.Entities.Subscribers
            {
                Email = ARequest.Email,
                Count = 0,
                IsActivated = true,
                LastUpdated = null,
                Registered = FDateTimeService.Now
            };

            await FDatabaseContext.Subscribers.AddAsync(LNewSubscriber, ACancellationToken);
            await FDatabaseContext.SaveChangesAsync(ACancellationToken);
            return await Task.FromResult(LNewSubscriber.Id);
        }
    }
}