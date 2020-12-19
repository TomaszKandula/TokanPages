using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Database;

namespace TokanPages.Backend.Cqrs.Handlers.Queries.Subscribers
{

    public class GetAllSubscribersQueryHandler : TemplateHandler<GetAllSubscribersQuery, IEnumerable<Domain.Entities.Subscribers>>
    {

        private readonly DatabaseContext FDatabaseContext;

        public GetAllSubscribersQueryHandler(DatabaseContext ADatabaseContext) 
        {
            FDatabaseContext = ADatabaseContext;
        }

        public override async Task<IEnumerable<Domain.Entities.Subscribers>> Handle(GetAllSubscribersQuery ARequest, CancellationToken ACancellationToken) 
        {
            return await FDatabaseContext.Subscribers.Select(Subscribers => Subscribers).ToListAsync(ACancellationToken);
        }

    }

}
