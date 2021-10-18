namespace TokanPages.Backend.Cqrs.Handlers.Queries.Subscribers
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Database;
    using Core.Logger;
    using Core.Exceptions;
    using Shared.Resources;

    public class GetSubscriberQueryHandler : TemplateHandler<GetSubscriberQuery, GetSubscriberQueryResult>
    {
        public GetSubscriberQueryHandler(DatabaseContext databaseContext, ILogger logger) : base(databaseContext, logger) { }

        public override async Task<GetSubscriberQueryResult> Handle(GetSubscriberQuery request, CancellationToken cancellationToken) 
        {
            var currentSubscriber = await DatabaseContext.Subscribers
                .AsNoTracking()
                .Where(subscribers => subscribers.Id == request.Id)
                .Select(subscribers => new GetSubscriberQueryResult 
                { 
                    Id = subscribers.Id,
                    Email = subscribers.Email,
                    IsActivated = subscribers.IsActivated,
                    NewsletterCount = subscribers.Count,
                    Registered = subscribers.Registered,
                    LastUpdated = subscribers.LastUpdated
                })
                .ToListAsync(cancellationToken);

            if (!currentSubscriber.Any()) 
                throw new BusinessException(nameof(ErrorCodes.SUBSCRIBER_DOES_NOT_EXISTS), ErrorCodes.SUBSCRIBER_DOES_NOT_EXISTS);

            return currentSubscriber.First();
        }
    }
}