using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Database;
using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Queries.Articles
{

    public class GetArticleCommandHandler : IRequestHandler<GetArticleCommand, Domain.Entities.Articles>
    {

        private readonly DatabaseContext FDatabaseContext;

        public GetArticleCommandHandler(DatabaseContext ADatabaseContext)
        {
            FDatabaseContext = ADatabaseContext;
        }

        public async Task<Domain.Entities.Articles> Handle(GetArticleCommand ARequest, CancellationToken ACancellationToken)
        {
            return (await FDatabaseContext.Articles
                .Where(R => R.Id == ARequest.Id)
                .Select(Articles => Articles)
                .ToListAsync(ACancellationToken))
                .FirstOrDefault();
        }

    }

}
