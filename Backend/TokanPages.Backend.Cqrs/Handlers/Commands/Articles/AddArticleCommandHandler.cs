using System;
using System.Threading;
using System.Threading.Tasks;
using TokanPages.Backend.Database;
using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Articles
{

    public class AddArticleCommandHandler : TemplateHandler<AddArticleCommand, Unit>
    {

        private readonly DatabaseContext FDatabaseContext;

        public AddArticleCommandHandler(DatabaseContext ADatabaseContext) 
        {
            FDatabaseContext = ADatabaseContext;
        }

        public override async Task<Unit> Handle(AddArticleCommand ARequest, CancellationToken ACancellationToken)
        {

            // Field 'ARequest.Text' should be used to create text.html 
            // and place on Azure Storage Blob under returned ID.
            // ...

            var LNewArticle = new Domain.Entities.Articles
            {
                Id = Guid.NewGuid(),
                Title = ARequest.Title,
                Description = ARequest.Desc,
                IsPublished = false,
                Likes = 0,
                ReadCount = 0
            };

            FDatabaseContext.Articles.Add(LNewArticle);
            await FDatabaseContext.SaveChangesAsync(ACancellationToken);
            return await Task.FromResult(Unit.Value);

        }

    }

}
