namespace TokanPages.Backend.Cqrs.Handlers.Commands.Articles
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Database;
    using Core.Exceptions;
    using Shared.Resources;
    using Services.UserProvider;
    using Shared.Services.DateTimeService;
    using Storage.AzureBlobStorage.Factory;
    using MediatR;

    public class UpdateArticleContentCommandHandler : TemplateHandler<UpdateArticleContentCommand, Unit>
    {
        private readonly DatabaseContext FDatabaseContext;

        private readonly IUserProvider FUserProvider;
        
        private readonly IDateTimeService FDateTimeService;
        
        private readonly IAzureBlobStorageFactory FAzureBlobStorageFactory;
        
        public UpdateArticleContentCommandHandler(DatabaseContext ADatabaseContext, IUserProvider AUserProvider, 
            IDateTimeService ADateTimeService, IAzureBlobStorageFactory AAzureBlobStorageFactory)
        {
            FDatabaseContext = ADatabaseContext;
            FUserProvider = AUserProvider;
            FDateTimeService = ADateTimeService;
            FAzureBlobStorageFactory = AAzureBlobStorageFactory;
        }

        public override async Task<Unit> Handle(UpdateArticleContentCommand ARequest, CancellationToken ACancellationToken)
        {
            var LUserId = await FUserProvider.GetUserId();
            if (LUserId == null)
                throw new BusinessException(nameof(ErrorCodes.ACCESS_DENIED), ErrorCodes.ACCESS_DENIED);

            var LArticles = await FDatabaseContext.Articles
                .Where(AArticles => AArticles.Id == ARequest.Id)
                .ToListAsync(ACancellationToken);

            if (!LArticles.Any())
                throw new BusinessException(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS), ErrorCodes.ARTICLE_DOES_NOT_EXISTS);
            
            var LAzureBlob = FAzureBlobStorageFactory.Create();
            if (!string.IsNullOrEmpty(ARequest.TextToUpload))
                await LAzureBlob.UploadText(ARequest.Id, ARequest.TextToUpload);

            if (!string.IsNullOrEmpty(ARequest.ImageToUpload))
                await LAzureBlob.UploadImage(ARequest.Id, ARequest.ImageToUpload);

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