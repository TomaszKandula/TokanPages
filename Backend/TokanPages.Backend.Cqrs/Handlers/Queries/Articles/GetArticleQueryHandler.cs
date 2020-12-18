using System.Threading;
using System.Threading.Tasks;
using TokanPages.Backend.Database;
using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Queries.Articles
{

    public class GetArticleQueryHandler : IRequestHandler<GetArticleQuery, Domain.Entities.Articles>
    {

        private readonly DatabaseContext FDatabaseContext;

        public GetArticleQueryHandler(DatabaseContext ADatabaseContext)
        {
            FDatabaseContext = ADatabaseContext;
        }

        public async Task<Domain.Entities.Articles> Handle(GetArticleQuery ARequest, CancellationToken ACancellationToken)
        {
            
            var LCurrentArticle = await FDatabaseContext.Articles.FindAsync(new object[] { ARequest.Id }, ACancellationToken);
            if (LCurrentArticle == null) 
            { 
                // TODO: add error call
            }

            return LCurrentArticle;

        }

    }

}
