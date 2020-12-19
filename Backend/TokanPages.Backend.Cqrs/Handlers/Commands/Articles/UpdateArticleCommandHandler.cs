using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using TokanPages.Backend.Storage;
using TokanPages.Backend.Database;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Resources;
using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Articles
{
    
    public class UpdateArticleCommandHandler : TemplateHandler<UpdateArticleCommand, Unit>
    {

        private readonly DatabaseContext FDatabaseContext;
        private readonly IAzureStorageService FAzureStorageService;

        public UpdateArticleCommandHandler(DatabaseContext ADatabaseContext, IAzureStorageService AAzureStorageService) 
        {
            FDatabaseContext = ADatabaseContext;
            FAzureStorageService = AAzureStorageService;
        }

        public override async Task<Unit> Handle(UpdateArticleCommand ARequest, CancellationToken ACancellationToken) 
        {

            var LCurrentArticle = await FDatabaseContext.Articles.FindAsync(ARequest.Id);
            if (LCurrentArticle == null) 
            {
                throw new BusinessException(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS), ErrorCodes.ARTICLE_DOES_NOT_EXISTS);
            }

            var LTempFileName = $"{ARequest.Id}.txt";

            var LBaseDirectory = AppDomain.CurrentDomain.BaseDirectory + "\\__upload";
            if (!Directory.Exists(LBaseDirectory))
            {
                Directory.CreateDirectory(LBaseDirectory);
            }

            var LTempFilePath = LBaseDirectory + $"\\{LTempFileName}";
            if (!File.Exists(LTempFilePath))
            {
                using var LFileToUpload = new StreamWriter(LTempFilePath, true);
                await LFileToUpload.WriteAsync(ARequest.Text);
            }

            var LResult = await FAzureStorageService.UploadTextFile($"tokanpages\\content\\articles\\{ARequest.Id}", "text.html", $"{LTempFilePath}");
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
