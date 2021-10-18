namespace TokanPages.Backend.Cqrs.Handlers.Queries.Subscribers
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using Database;
    using Core.Logger;

    public class GetAllSubscribersQueryHandler : TemplateHandler<GetAllSubscribersQuery, IEnumerable<GetAllSubscribersQueryResult>>
    {
        public GetAllSubscribersQueryHandler(DatabaseContext databaseContext, ILogger logger) : base(databaseContext, logger) { }

        public override async Task<IEnumerable<GetAllSubscribersQueryResult>> Handle(GetAllSubscribersQuery request, CancellationToken cancellationToken) 
        {
            var subscribers = await DatabaseContext.Subscribers
                .AsNoTracking()
                .Select(subscribers => new GetAllSubscribersQueryResult 
                { 
                    Id = subscribers.Id,
                    Email = subscribers.Email,
                    IsActivated = subscribers.IsActivated,
                    NewsletterCount = subscribers.Count
                })
                .ToListAsync(cancellationToken);
            
            return subscribers;
        }
    }
}