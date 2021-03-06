using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Database;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Cqrs.Handlers.Queries.Subscribers
{
    public class GetSubscriberQueryHandler : TemplateHandler<GetSubscriberQuery, GetSubscriberQueryResult>
    {
        private readonly DatabaseContext FDatabaseContext;

        public GetSubscriberQueryHandler(DatabaseContext ADatabaseContext) 
            => FDatabaseContext = ADatabaseContext;

        public override async Task<GetSubscriberQueryResult> Handle(GetSubscriberQuery ARequest, CancellationToken ACancellationToken) 
        {
            var LCurrentSubscriber = await FDatabaseContext.Subscribers
                .AsNoTracking()
                .Where(ASubscribers => ASubscribers.Id == ARequest.Id)
                .Select(AFields => new GetSubscriberQueryResult 
                { 
                    Id = AFields.Id,
                    Email = AFields.Email,
                    IsActivated = AFields.IsActivated,
                    NewsletterCount = AFields.Count,
                    Registered = AFields.Registered,
                    LastUpdated = AFields.LastUpdated
                })
                .ToListAsync(ACancellationToken);

            if (!LCurrentSubscriber.Any()) 
                throw new BusinessException(nameof(ErrorCodes.SUBSCRIBER_DOES_NOT_EXISTS), ErrorCodes.SUBSCRIBER_DOES_NOT_EXISTS);

            return LCurrentSubscriber.First();
        }
    }
}
