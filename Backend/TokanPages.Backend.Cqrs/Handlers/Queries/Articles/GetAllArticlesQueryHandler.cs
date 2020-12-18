using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Database;
using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Queries.Articles
{

    public class GetAllArticlesQueryHandler : IRequestHandler<GetAllArticlesQuery, IEnumerable<Domain.Entities.Articles>>
    {

        private readonly DatabaseContext FDatabaseContext;

        public GetAllArticlesQueryHandler(DatabaseContext ADatabaseContext) 
        {
            FDatabaseContext = ADatabaseContext;
        }

        public async Task<IEnumerable<Domain.Entities.Articles>> Handle(GetAllArticlesQuery ARequest, CancellationToken ACancellationToken) 
        {
            return await FDatabaseContext.Articles.Select(Articles => Articles).ToListAsync(ACancellationToken);
        }

    }

}
