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
    using Core.Utilities.DateTimeService;

    public class AddSubscriberCommandHandler : TemplateHandler<AddSubscriberCommand, Guid>
    {
        private readonly DatabaseContext _databaseContext;
        
        private readonly IDateTimeService _dateTimeService;
        
        public AddSubscriberCommandHandler(DatabaseContext databaseContext, IDateTimeService dateTimeService) 
        {
            _databaseContext = databaseContext;
            _dateTimeService = dateTimeService;
        }

        public override async Task<Guid> Handle(AddSubscriberCommand request, CancellationToken cancellationToken) 
        {
            var emailCollection = await _databaseContext.Subscribers
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

            await _databaseContext.Subscribers.AddAsync(newSubscriber, cancellationToken);
            await _databaseContext.SaveChangesAsync(cancellationToken);
            return await Task.FromResult(newSubscriber.Id);
        }
    }
}