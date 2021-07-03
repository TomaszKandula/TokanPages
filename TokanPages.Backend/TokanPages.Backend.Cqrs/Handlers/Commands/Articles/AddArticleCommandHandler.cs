using System;
using System.Threading;
using System.Threading.Tasks;
using TokanPages.Backend.Database;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Cqrs.Services.UserProvider;
using TokanPages.Backend.Shared.Services.DateTimeService;
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

            var LNewArticle = new Domain.Entities.Articles
            {
                Title = ARequest.Title,
                Description = ARequest.Description,
                IsPublished = false,
                ReadCount = 0,
                CreatedAt = FDateTimeService.Now,
                UpdatedAt = null,
                // ReSharper disable once PossibleInvalidOperationException
                // GetUserId is already check for null value
                UserId = (Guid) FUserProvider.GetUserId()
            };

            await FDatabaseContext.Articles.AddAsync(LNewArticle, ACancellationToken);
            await FDatabaseContext.SaveChangesAsync(ACancellationToken);
            
            var LAzureBlob = FAzureBlobStorageFactory.Create();
            await LAzureBlob.UploadText(LNewArticle.Id, ARequest.TextToUpload);
            await LAzureBlob.UploadImage(LNewArticle.Id, ARequest.ImageToUpload);
            
            return await Task.FromResult(LNewArticle.Id);
        }
    }
}
