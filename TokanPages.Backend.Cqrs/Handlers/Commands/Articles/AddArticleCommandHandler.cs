using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using TokanPages.Backend.Database;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Cqrs.Services.UserProvider;
using TokanPages.Backend.Core.Services.DateTimeService;
using TokanPages.Backend.Storage.AzureBlobStorage.Factory;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Articles
{
    public class AddArticleCommandHandler : TemplateHandler<AddArticleCommand, Guid>
    {
        private readonly DatabaseContext FDatabaseContext;
        
        private readonly IUserProvider FUserProvider;
        
        private readonly IDateTimeService FDateTimeService;
        
        private readonly IAzureBlobStorageFactory FAzureBlobStorageFactory;
        
        public AddArticleCommandHandler(DatabaseContext ADatabaseContext, IUserProvider AUserProvider, 
            IDateTimeService ADateTimeService, IAzureBlobStorageFactory AAzureBlobStorageFactory) 
        {
            FDatabaseContext = ADatabaseContext;
            FUserProvider = AUserProvider;
            FDateTimeService = ADateTimeService;
            FAzureBlobStorageFactory = AAzureBlobStorageFactory;
        }

        public override async Task<Guid> Handle(AddArticleCommand ARequest, CancellationToken ACancellationToken)
        {
            if (FUserProvider.GetUserId() == null)
                throw new BusinessException(nameof(ErrorCodes.ACCESS_DENIED), ErrorCodes.ACCESS_DENIED);
            
            var LNewId = Guid.NewGuid();

            await UploadText(LNewId, ARequest.TextToUpload);
            await UploadImage(LNewId, ARequest.ImageToUpload);

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

        private async Task UploadText(Guid AId, string ATextToUpload) // TODO: refactor to shared
        {
            var LAzureBlob = FAzureBlobStorageFactory.Create();
            var LTextToBase64 = ATextToUpload.ToBase64Encode();
            var LBytes = Convert.FromBase64String(LTextToBase64);
            var LContents = new MemoryStream(LBytes);

            try
            {
                var LDestinationPath = $"content\\articles\\{AId.ToString().ToLower()}\\text.json";
                await LAzureBlob.UploadFile(LContents, LDestinationPath);
            }
            catch (Exception LException)
            {
                throw new BusinessException(nameof(ErrorCodes.CANNOT_SAVE_TO_AZURE_STORAGE), LException.Message);
            }
        }

        private async Task UploadImage(Guid AId, string AImageToUpload) // TODO: refactor to shared
        {
            if (!AImageToUpload.IsBase64String()) 
                throw new BusinessException(nameof(ErrorCodes.INVALID_BASE64), ErrorCodes.INVALID_BASE64);
            
            var LAzureBlob = FAzureBlobStorageFactory.Create();
            var LBytes = Convert.FromBase64String(AImageToUpload);
            var LContents = new MemoryStream(LBytes);
            
            try
            {
                var LDestinationPath = $"content\\articles\\{AId.ToString().ToLower()}\\image.jpeg";
                await LAzureBlob.UploadFile(LContents, LDestinationPath);
            }
            catch (Exception LException)
            {
                throw new BusinessException(nameof(ErrorCodes.CANNOT_SAVE_TO_AZURE_STORAGE), LException.Message);
            }
        }
    }
}
