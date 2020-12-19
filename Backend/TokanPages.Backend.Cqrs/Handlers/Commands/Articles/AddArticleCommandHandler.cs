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

    public class AddArticleCommandHandler : TemplateHandler<AddArticleCommand, Unit>
    {

        private readonly DatabaseContext FDatabaseContext;
        private readonly IAzureStorageService FAzureStorageService;

        public AddArticleCommandHandler(DatabaseContext ADatabaseContext, IAzureStorageService AAzureStorageService) 
        {
            FDatabaseContext = ADatabaseContext;
            FAzureStorageService = AAzureStorageService;
        }

        public override async Task<Unit> Handle(AddArticleCommand ARequest, CancellationToken ACancellationToken)
        {

            var LNewId = Guid.NewGuid();
            var LTempFileName = $"{LNewId}.txt";

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

            var LResult = await FAzureStorageService.UploadTextFile($"tokanpages\\content\\articles\\{LNewId}", "text.html", $"{LTempFilePath}");
            if (!LResult.IsSucceeded) 
            {
                throw new BusinessException(nameof(ErrorCodes.CANNOT_SAVE_TO_AZURE_STORAGE), LResult.ErrorDesc);
            }

            var LNewArticle = new Domain.Entities.Articles
            {
                Id = LNewId,
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
