using System.Threading;
using System.Threading.Tasks;
using TokanPages.Backend.Storage;
using TokanPages.Backend.Database;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Core.Services.FileUtility;
using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Articles
{
    
    public class UpdateArticleCommandHandler : TemplateHandler<UpdateArticleCommand, Unit>
    {

        private readonly DatabaseContext FDatabaseContext;
        private readonly IAzureStorageService FAzureStorageService;
        private readonly IFileUtility FFileUtility;

        public UpdateArticleCommandHandler(DatabaseContext ADatabaseContext, IAzureStorageService AAzureStorageService, IFileUtility AFileUtility) 
        {
            FDatabaseContext = ADatabaseContext;
            FAzureStorageService = AAzureStorageService;
            FFileUtility = AFileUtility;
        }

        public override async Task<Unit> Handle(UpdateArticleCommand ARequest, CancellationToken ACancellationToken) 
        {

            var LCurrentArticle = await FDatabaseContext.Articles.FindAsync(ARequest.Id);
            if (LCurrentArticle == null) 
            {
                throw new BusinessException(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS), ErrorCodes.ARTICLE_DOES_NOT_EXISTS);
            }

            var LTempFile = await FFileUtility.SaveToFile("__upload", ARequest.Id.ToString(), ARequest.Text);
            var LResult = await FAzureStorageService.UploadFile($"tokanpages\\content\\articles\\{ARequest.Id}", "text.html", $"{LTempFile}", "text/html", ACancellationToken);
            if (!LResult.IsSucceeded)
            {
                throw new BusinessException(nameof(ErrorCodes.CANNOT_SAVE_TO_AZURE_STORAGE), LResult.ErrorDesc);
            }

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
