using System;
using System.Threading;
using System.Threading.Tasks;
using TokanPages.Backend.Database;
using TokanPages.Backend.Storage.Models;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Storage.AzureStorage;
using TokanPages.Backend.Core.Services.FileUtility;
using TokanPages.Backend.Core.Services.DateTimeService;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Articles
{
    public class AddArticleCommandHandler : TemplateHandler<AddArticleCommand, Guid>
    {
        private readonly DatabaseContext FDatabaseContext;
        private readonly IAzureStorageService FAzureStorageService;
        private readonly IFileUtilityService FFileUtilityService;
        private readonly IDateTimeService FDateTimeService;

        public AddArticleCommandHandler(DatabaseContext ADatabaseContext, IAzureStorageService AAzureStorageService, 
            IFileUtilityService AFileUtilityService, IDateTimeService ADateTimeService) 
        {
            FDatabaseContext = ADatabaseContext;
            FAzureStorageService = AAzureStorageService;
            FFileUtilityService = AFileUtilityService;
            FDateTimeService = ADateTimeService;
        }

        public override async Task<Guid> Handle(AddArticleCommand ARequest, CancellationToken ACancellationToken)
        {
            var LNewId = Guid.NewGuid();

            var LUploadText = await UploadText(LNewId, ARequest.TextToUpload, ACancellationToken);
            if (!LUploadText.IsSucceeded) 
                throw new BusinessException(nameof(ErrorCodes.CANNOT_SAVE_TO_AZURE_STORAGE), LUploadText.ErrorDesc);

            var LUploadImage = await UploadImage(LNewId, ARequest.ImageToUpload, ACancellationToken);
            if (!LUploadImage.IsSucceeded)
                throw new BusinessException(nameof(ErrorCodes.CANNOT_SAVE_TO_AZURE_STORAGE), LUploadImage.ErrorDesc);

            FDatabaseContext.Articles.Add(new Domain.Entities.Articles
            {
                Id = LNewId,
                Title = ARequest.Title,
                Description = ARequest.Description,
                IsPublished = false,
                ReadCount = 0,
                CreatedAt = FDateTimeService.Now,
                UpdatedAt = null
            });

            await FDatabaseContext.SaveChangesAsync(ACancellationToken);
            return await Task.FromResult(LNewId);
        }

        private async Task<ActionResult> UploadText(Guid AId, string ATextToUpload, CancellationToken ACancellationToken)
        {
            var LTextContent = await FFileUtilityService
                .SaveToFile("__upload", $"{AId}.json", ATextToUpload);

            return await FAzureStorageService
                .UploadFile(
                    $"content\\articles\\{AId.ToString().ToLower()}", 
                    "text.json", 
                    LTextContent, 
                    "application/json", 
                    ACancellationToken);
        }

        private async Task<ActionResult> UploadImage(Guid AId, string AImageToUpload, CancellationToken ACancellationToken)
        {
            if (!AImageToUpload.IsBase64String()) 
                throw new BusinessException(nameof(ErrorCodes.INVALID_BASE64), ErrorCodes.INVALID_BASE64);
            
            var LImageContent = await FFileUtilityService
                .SaveToFile("__upload", $"{AId}.jpg", AImageToUpload);

            return await FAzureStorageService
                .UploadFile(
                    $"content\\articles\\{AId.ToString().ToLower()}", 
                    "image.jpeg", 
                    LImageContent, "image/jpeg", ACancellationToken);
        }
    }
}
