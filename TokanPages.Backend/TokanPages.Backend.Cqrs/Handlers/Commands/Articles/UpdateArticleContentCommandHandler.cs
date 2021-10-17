namespace TokanPages.Backend.Cqrs.Handlers.Commands.Articles
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Database;
    using Core.Exceptions;
    using Shared.Resources;
    using Services.UserServiceProvider;
    using Core.Utilities.DateTimeService;
    using Storage.AzureBlobStorage.Factory;
    using MediatR;

    public class UpdateArticleContentCommandHandler : TemplateHandler<UpdateArticleContentCommand, Unit>
    {
        private readonly DatabaseContext FDatabaseContext;

        private readonly IUserServiceProvider FUserServiceProvider;
        
        private readonly IDateTimeService FDateTimeService;
        
        private readonly IAzureBlobStorageFactory FAzureBlobStorageFactory;
        
        public UpdateArticleContentCommandHandler(DatabaseContext ADatabaseContext, IUserServiceProvider AUserServiceProvider, 
            IDateTimeService ADateTimeService, IAzureBlobStorageFactory AAzureBlobStorageFactory)
        {
            FDatabaseContext = ADatabaseContext;
            FUserServiceProvider = AUserServiceProvider;
            FDateTimeService = ADateTimeService;
            FAzureBlobStorageFactory = AAzureBlobStorageFactory;
        }

        public override async Task<Unit> Handle(UpdateArticleContentCommand ARequest, CancellationToken ACancellationToken)
        {
            var LUserId = await FUserServiceProvider.GetUserId();
            if (LUserId == null)
                throw new BusinessException(nameof(ErrorCodes.ACCESS_DENIED), ErrorCodes.ACCESS_DENIED);

            var LArticles = await FDatabaseContext.Articles
                .Where(AArticles => AArticles.Id == ARequest.Id)
                .ToListAsync(ACancellationToken);

            if (!LArticles.Any())
                throw new BusinessException(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS), ErrorCodes.ARTICLE_DOES_NOT_EXISTS);

            var LAzureBlob = FAzureBlobStorageFactory.Create();
            if (!string.IsNullOrEmpty(ARequest.TextToUpload))
            {
                var LTextDestinationPath = $"content\\articles\\{ARequest.Id}\\text.json";
                await LAzureBlob.UploadContent(ARequest.TextToUpload, LTextDestinationPath, ACancellationToken);
            }

            if (!string.IsNullOrEmpty(ARequest.ImageToUpload))
            {
                var LImageDestinationPath = $"content\\articles\\{ARequest.Id}\\image.jpg";
                await LAzureBlob.UploadContent(ARequest.ImageToUpload, LImageDestinationPath, ACancellationToken);
            }

            var LCurrentArticle = LArticles.First();

            LCurrentArticle.Title = ARequest.Title ?? LCurrentArticle.Title;
            LCurrentArticle.Description = ARequest.Description ?? LCurrentArticle.Description;
            LCurrentArticle.UpdatedAt = ARequest.Title != null && ARequest.Description != null
                ? FDateTimeService.Now
                : LCurrentArticle.UpdatedAt;

            await FDatabaseContext.SaveChangesAsync(ACancellationToken);
            return await Task.FromResult(Unit.Value);
        }
    }
}