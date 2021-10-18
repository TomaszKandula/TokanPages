namespace TokanPages.Backend.Cqrs.Handlers.Commands.Subscribers
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Database;
    using Core.Logger;
    using Core.Exceptions;
    using Shared.Resources;
    using Core.Utilities.DateTimeService;

    public class AddSubscriberCommandHandler : TemplateHandler<AddSubscriberCommand, Guid>
    {
        private readonly IDateTimeService _dateTimeService;
        
        public AddSubscriberCommandHandler(DatabaseContext databaseContext, ILogger logger, 
            IDateTimeService dateTimeService) : base(databaseContext, logger)
        {
            _dateTimeService = dateTimeService;
        }

        public override async Task<Guid> Handle(AddSubscriberCommand request, CancellationToken cancellationToken) 
        {
            var emailCollection = await DatabaseContext.Subscribers
                .AsNoTracking()
                .Where(subscribers => subscribers.Email == request.Email)
                .ToListAsync(cancellationToken);

            if (emailCollection.Count == 1)
                throw new BusinessException(nameof(ErrorCodes.EMAIL_ADDRESS_ALREADY_EXISTS), ErrorCodes.EMAIL_ADDRESS_ALREADY_EXISTS);

            var newSubscriber = new Domain.Entities.Subscribers
            {
                Email = request.Email,
                Count = 0,
                IsActivated = true,
                LastUpdated = null,
                Registered = _dateTimeService.Now
            };

            await DatabaseContext.Subscribers.AddAsync(newSubscriber, cancellationToken);
            await DatabaseContext.SaveChangesAsync(cancellationToken);
            return await Task.FromResult(newSubscriber.Id);
        }
    }
}