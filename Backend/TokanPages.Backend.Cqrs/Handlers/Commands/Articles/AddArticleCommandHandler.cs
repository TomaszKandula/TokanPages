using System;
using System.Threading;
using System.Threading.Tasks;
using TokanPages.Backend.Storage;
using TokanPages.Backend.Database;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Core.Services.FileUtility;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Articles
{

    public class AddArticleCommandHandler : TemplateHandler<AddArticleCommand, Guid>
    {

        private readonly DatabaseContext FDatabaseContext;
        private readonly IAzureStorageService FAzureStorageService;
        private readonly IFileUtility FFileUtility;

        public AddArticleCommandHandler(DatabaseContext ADatabaseContext, 
            IAzureStorageService AAzureStorageService, IFileUtility AFileUtility) 
        {
            FDatabaseContext = ADatabaseContext;
            FAzureStorageService = AAzureStorageService;
            FFileUtility = AFileUtility;
        }

        public override async Task<Guid> Handle(AddArticleCommand ARequest, CancellationToken ACancellationToken)
        {

            var LImageBase64Check = FFileUtility.IsBase64String(ARequest.ImageToUpload);
            if (!LImageBase64Check) 
            {
                throw new BusinessException(nameof(ErrorCodes.INVALID_BASE64), ErrorCodes.INVALID_BASE64);
            }

            var LNewId = Guid.NewGuid();

            var LTextContent = await FFileUtility.SaveToFile("__upload", $"{LNewId}.json", ARequest.TextToUpload);
            var LTextUpload = await FAzureStorageService.UploadFile($"content\\articles\\{LNewId.ToString().ToLower()}", "text.json", LTextContent, "application/json", ACancellationToken);
            if (!LTextUpload.IsSucceeded) 
            {
                throw new BusinessException(nameof(ErrorCodes.CANNOT_SAVE_TO_AZURE_STORAGE), LTextUpload.ErrorDesc);
            }

            var LImageContent = await FFileUtility.SaveToFile("__upload", $"{LNewId}.jpg", ARequest.ImageToUpload);
            var LImageUpload = await FAzureStorageService.UploadFile($"content\\articles\\{LNewId.ToString().ToLower()}", "image.jpeg", LImageContent, "image/jpeg", ACancellationToken);
            if (!LImageUpload.IsSucceeded)
            {
                throw new BusinessException(nameof(ErrorCodes.CANNOT_SAVE_TO_AZURE_STORAGE), LImageUpload.ErrorDesc);
            }

            FDatabaseContext.Articles.Add(new Domain.Entities.Articles
            {
                Id = LNewId,
                Title = ARequest.Title,
                Description = ARequest.Description,
                IsPublished = false,
                Likes = 0,
                ReadCount = 0,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null
            });

            await FDatabaseContext.SaveChangesAsync(ACancellationToken);
            return await Task.FromResult(LNewId);

        }

    }

}
