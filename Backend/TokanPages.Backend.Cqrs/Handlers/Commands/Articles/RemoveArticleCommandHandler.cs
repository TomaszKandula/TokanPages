using System.Threading;
using System.Threading.Tasks;
using TokanPages.Backend.Database;
using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Articles
{

    public class RemoveArticleCommandHandler : IRequestHandler<RemoveArticleCommand, Unit>
    {

        private readonly DatabaseContext FDatabaseContext;

        public RemoveArticleCommandHandler(DatabaseContext ADatabaseContext) 
        {
            FDatabaseContext = ADatabaseContext;
        }

        public async Task<Unit> Handle(RemoveArticleCommand ARequest, CancellationToken ACancellationToken) 
        {

            var LCurrentArticle = await FDatabaseContext.Articles.FindAsync(ARequest.Id);
            if (LCurrentArticle == null)
            {
                // TODO: add error call
            }

            FDatabaseContext.Articles.Remove(LCurrentArticle);

            await FDatabaseContext.SaveChangesAsync(ACancellationToken);
            return await Task.FromResult(Unit.Value);

        }

    }

}
