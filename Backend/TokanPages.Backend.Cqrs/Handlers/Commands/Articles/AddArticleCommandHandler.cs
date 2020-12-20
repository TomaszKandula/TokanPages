using System;
using System.Threading;
using System.Threading.Tasks;
using TokanPages.Backend.Storage;
using TokanPages.Backend.Database;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Core.FileUtility;
using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Articles
{

    public class AddArticleCommandHandler : TemplateHandler<AddArticleCommand, Unit>
    {

        private readonly DatabaseContext FDatabaseContext;
        private readonly IAzureStorageService FAzureStorageService;
        private readonly IFileUtility FFileUtility;

        public AddArticleCommandHandler(DatabaseContext ADatabaseContext, IAzureStorageService AAzureStorageService, IFileUtility AFileUtility) 
        {
            FDatabaseContext = ADatabaseContext;
            FAzureStorageService = AAzureStorageService;
            FFileUtility = AFileUtility;
        }

        public override async Task<Unit> Handle(AddArticleCommand ARequest, CancellationToken ACancellationToken)
        {

            var LNewId = Guid.NewGuid();
            var LTempFile = await FFileUtility.SaveToFile("__upload", LNewId.ToString(), ARequest.Text);

            var LResult = await FAzureStorageService.UploadFile($"tokanpages\\content\\articles\\{LNewId}", "text.html", $"{LTempFile}", "text/html", ACancellationToken);
            if (!LResult.IsSucceeded) 
            {
                throw new BusinessException(nameof(ErrorCodes.CANNOT_SAVE_TO_AZURE_STORAGE), LResult.ErrorDesc);
            }

            var LNewArticle = new Domain.Entities.Articles
            {
                Id = LNewId,
                Title = ARequest.Title,
                Description = ARequest.Description,
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
