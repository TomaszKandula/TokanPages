using System.Threading;
using System.Threading.Tasks;
using TokanPages.Backend.Database;
using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Articles
{
    
    public class UpdateArticleCommandHandler : IRequestHandler<UpdateArticleCommand, Unit>
    {

        private readonly DatabaseContext FDatabaseContext;

        public UpdateArticleCommandHandler(DatabaseContext ADatabaseContext) 
        {
            FDatabaseContext = ADatabaseContext;
        }

        public async Task<Unit> Handle(UpdateArticleCommand ARequest, CancellationToken ACancellationToken) 
        {

            var LCurrentArticle = await FDatabaseContext.Articles.FindAsync(ARequest.Id);
            if (LCurrentArticle == null) 
            { 
                // TODO: add error call
            }

            // Field 'ARequest.Text' should be used to update text.html 
            // that resides on Azure Storage Blob under returned ID.
            // ...

            LCurrentArticle.Title = ARequest.Title;
            LCurrentArticle.Description = ARequest.Description;
            LCurrentArticle.IsPublished = ARequest.IsPublished;
            LCurrentArticle.Likes = ARequest.Likes;
            LCurrentArticle.ReadCount = ARequest.ReadCount;

            await FDatabaseContext.SaveChangesAsync(ACancellationToken);
            return await Task.FromResult(Unit.Value);
        
        }

    }

}
