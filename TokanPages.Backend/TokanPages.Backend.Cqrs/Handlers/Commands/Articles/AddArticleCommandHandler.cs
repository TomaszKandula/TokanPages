namespace TokanPages.Backend.Cqrs.Handlers.Commands.Articles
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Database;
    using Core.Exceptions;
    using Shared.Resources;
    using Services.UserProvider;
    using Shared.Services.DateTimeService;
    using Storage.AzureBlobStorage.Factory;

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
            var LUserId = await FUserProvider.GetUserId();
            if (LUserId == null)
                throw new BusinessException(nameof(ErrorCodes.ACCESS_DENIED), ErrorCodes.ACCESS_DENIED);

            var LNewArticle = new Domain.Entities.Articles
            {
                Title = ARequest.Title,
                Description = ARequest.Description,
                IsPublished = false,
                ReadCount = 0,
                CreatedAt = FDateTimeService.Now,
                UpdatedAt = null,
                UserId = (Guid) LUserId
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