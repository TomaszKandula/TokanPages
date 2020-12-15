using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Database;
using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Queries.Articles
{

    public class GetAllArticlesCommandHandler : IRequestHandler<GetAllArticlesCommand, IEnumerable<Domain.Entities.Articles>>
    {

        private readonly DatabaseContext FDatabaseContext;

        public GetAllArticlesCommandHandler(DatabaseContext ADatabaseContext) 
        {
            FDatabaseContext = ADatabaseContext;
        }

        public async Task<IEnumerable<Domain.Entities.Articles>> Handle(GetAllArticlesCommand ARequest, CancellationToken ACancellationToken) 
        {
            return await FDatabaseContext.Articles.Select(Articles => Articles).ToListAsync(ACancellationToken);
        }

    }

}
