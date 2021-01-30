using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Database;

namespace TokanPages.Backend.Cqrs.Handlers.Queries.Subscribers
{
    public class GetAllSubscribersQueryHandler : TemplateHandler<GetAllSubscribersQuery, IEnumerable<GetAllSubscribersQueryResult>>
    {
        private readonly DatabaseContext FDatabaseContext;

        public GetAllSubscribersQueryHandler(DatabaseContext ADatabaseContext) 
        {
            FDatabaseContext = ADatabaseContext;
        }

        public override async Task<IEnumerable<GetAllSubscribersQueryResult>> Handle(GetAllSubscribersQuery ARequest, CancellationToken ACancellationToken) 
        {
            var LSubscribers = await FDatabaseContext.Subscribers
                .AsNoTracking()
                .Select(Fields => new GetAllSubscribersQueryResult 
                { 
                    Id = Fields.Id,
                    Email = Fields.Email,
                    IsActivated = Fields.IsActivated,
                    NewsletterCount = Fields.Count
                })
                .ToListAsync(ACancellationToken);
            
            return LSubscribers;
        }
    }
}
