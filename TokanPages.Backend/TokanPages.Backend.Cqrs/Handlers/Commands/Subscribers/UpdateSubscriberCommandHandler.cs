﻿namespace TokanPages.Backend.Cqrs.Handlers.Commands.Subscribers
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Database;
    using Core.Exceptions;
    using Shared.Resources;
    using Core.Utilities.DateTimeService;
    using MediatR;

    public class UpdateSubscriberCommandHandler : TemplateHandler<UpdateSubscriberCommand, Unit>
    {
        private readonly DatabaseContext _databaseContext;
        
        private readonly IDateTimeService _dateTimeService;
        
        public UpdateSubscriberCommandHandler(DatabaseContext databaseContext, IDateTimeService dateTimeService) 
        {
            _databaseContext = databaseContext;
            _dateTimeService = dateTimeService;
        }

        public override async Task<Unit> Handle(UpdateSubscriberCommand request, CancellationToken cancellationToken) 
        {
            var subscribersList = await _databaseContext.Subscribers
                .Where(subscribers => subscribers.Id == request.Id)
                .ToListAsync(cancellationToken);

            if (!subscribersList.Any()) 
                throw new BusinessException(nameof(ErrorCodes.SUBSCRIBER_DOES_NOT_EXISTS), ErrorCodes.SUBSCRIBER_DOES_NOT_EXISTS);

            var emailCollection = await _databaseContext.Subscribers
                .AsNoTracking()
                .Where(subscribers => subscribers.Email == request.Email)
                .ToListAsync(cancellationToken);

            if (emailCollection.Count == 1)
                throw new BusinessException(nameof(ErrorCodes.EMAIL_ADDRESS_ALREADY_EXISTS), ErrorCodes.EMAIL_ADDRESS_ALREADY_EXISTS);

            var currentSubscriber = subscribersList.First();

            currentSubscriber.Email = request.Email;
            currentSubscriber.Count = request.Count ?? currentSubscriber.Count;
            currentSubscriber.IsActivated = request.IsActivated ?? currentSubscriber.IsActivated;
            currentSubscriber.LastUpdated = _dateTimeService.Now;

            await _databaseContext.SaveChangesAsync(cancellationToken);
            return await Task.FromResult(Unit.Value);
        }
    }
}