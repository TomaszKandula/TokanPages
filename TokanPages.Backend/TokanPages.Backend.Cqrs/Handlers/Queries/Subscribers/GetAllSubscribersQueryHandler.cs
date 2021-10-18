﻿namespace TokanPages.Backend.Cqrs.Handlers.Queries.Subscribers
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using Database;

    public class GetAllSubscribersQueryHandler : TemplateHandler<GetAllSubscribersQuery, IEnumerable<GetAllSubscribersQueryResult>>
    {
        private readonly DatabaseContext _databaseContext;

        public GetAllSubscribersQueryHandler(DatabaseContext databaseContext) => _databaseContext = databaseContext;

        public override async Task<IEnumerable<GetAllSubscribersQueryResult>> Handle(GetAllSubscribersQuery request, CancellationToken cancellationToken) 
        {
            var subscribers = await _databaseContext.Subscribers
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