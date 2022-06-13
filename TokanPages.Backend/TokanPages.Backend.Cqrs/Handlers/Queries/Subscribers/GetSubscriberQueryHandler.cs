namespace TokanPages.Backend.Cqrs.Handlers.Queries.Subscribers;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Database;
using Core.Exceptions;
using Shared.Resources;
using Core.Utilities.LoggerService;

public class GetSubscriberQueryHandler : RequestHandler<GetSubscriberQuery, GetSubscriberQueryResult>
{
    public GetSubscriberQueryHandler(DatabaseContext databaseContext, ILoggerService loggerService) : base(databaseContext, loggerService) { }

    public override async Task<GetSubscriberQueryResult> Handle(GetSubscriberQuery request, CancellationToken cancellationToken) 
    {
        var subscriber = await DatabaseContext.Subscribers
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
            .SingleOrDefaultAsync(cancellationToken);

        if (subscriber is null) 
            throw new BusinessException(nameof(ErrorCodes.SUBSCRIBER_DOES_NOT_EXISTS), ErrorCodes.SUBSCRIBER_DOES_NOT_EXISTS);

        return subscriber;
    }
}