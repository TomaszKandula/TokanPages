using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Shared;
using TokanPages.Backend.Database;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Cqrs.Services.UserProvider;
using TokanPages.Backend.Shared.Services.DateTimeService;
using TokanPages.Backend.Storage.AzureBlobStorage.Factory;
using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Articles
{
    public class UpdateArticleCommandHandler : TemplateHandler<UpdateArticleCommand, Unit>
    {
        private readonly DatabaseContext FDatabaseContext;

        private readonly IUserProvider FUserProvider;
        
        private readonly IDateTimeService FDateTimeService;
        
        private readonly IAzureBlobStorageFactory FAzureBlobStorageFactory;
        
        public UpdateArticleCommandHandler(DatabaseContext ADatabaseContext, IUserProvider AUserProvider, 
            IDateTimeService ADateTimeService, IAzureBlobStorageFactory AAzureBlobStorageFactory)
        {
            FDatabaseContext = ADatabaseContext;
            FUserProvider = AUserProvider;
            FDateTimeService = ADateTimeService;
            FAzureBlobStorageFactory = AAzureBlobStorageFactory;
        }

        public override async Task<Unit> Handle(UpdateArticleCommand ARequest, CancellationToken ACancellationToken)
        {
            var LArticles = await FDatabaseContext.Articles
                .Where(AArticles => AArticles.Id == ARequest.Id)
                .ToListAsync(ACancellationToken);

            if (!LArticles.Any())
                throw new BusinessException(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS), ErrorCodes.ARTICLE_DOES_NOT_EXISTS);

            var LUserId = await FUserProvider.GetUserId();
            var LIsAnonymousUser = LUserId == null;
            
            var LAzureBlob = FAzureBlobStorageFactory.Create();
            if (!string.IsNullOrEmpty(ARequest.TextToUpload) && !LIsAnonymousUser)
                await LAzureBlob.UploadText(ARequest.Id, ARequest.TextToUpload);

            if (!string.IsNullOrEmpty(ARequest.ImageToUpload) && !LIsAnonymousUser)
                await LAzureBlob.UploadImage(ARequest.Id, ARequest.ImageToUpload);

            var LCurrentArticle = LArticles.First();

            if (!LIsAnonymousUser)
            {
                LCurrentArticle.Title = ARequest.Title ?? LCurrentArticle.Title;
                LCurrentArticle.Description = ARequest.Description ?? LCurrentArticle.Description;
                LCurrentArticle.IsPublished = ARequest.IsPublished ?? LCurrentArticle.IsPublished;
                LCurrentArticle.UpdatedAt = ARequest.Title != null && ARequest.Description != null
                    ? FDateTimeService.Now
                    : LCurrentArticle.UpdatedAt;
            }

            LCurrentArticle.ReadCount = ARequest.UpReadCount is true
                ? LCurrentArticle.ReadCount + 1 
                : LCurrentArticle.ReadCount;

            var LArticleLikes = await FDatabaseContext.ArticleLikes
                .Where(ALikes => ALikes.ArticleId == ARequest.Id)
                .WhereIfElse(LIsAnonymousUser,
                    ALikes => ALikes.IpAddress == FUserProvider.GetRequestIpAddress(),
                    ALikes => ALikes.UserId == LUserId)
                .ToListAsync(ACancellationToken);

            if (!LArticleLikes.Any())
            {
                await AddNewArticleLikes(LIsAnonymousUser, ARequest, ACancellationToken);
            }
            else
            {
                UpdateCurrentArticleLikes(LIsAnonymousUser, LArticleLikes.First(), ARequest.AddToLikes);
            }

            await FDatabaseContext.SaveChangesAsync(ACancellationToken);
            return await Task.FromResult(Unit.Value);
        }
        
        private async Task AddNewArticleLikes(bool AIsAnonymousUser, UpdateArticleCommand ARequest, CancellationToken ACancellationToken)
        {
            var LLikesLimit = AIsAnonymousUser 
                ? Constants.Likes.LIKES_LIMIT_FOR_ANONYMOUS 
                : Constants.Likes.LIKES_LIMIT_FOR_USER;

            var LEntity = new Domain.Entities.ArticleLikes
            {
                ArticleId = ARequest.Id,
                UserId = await FUserProvider.GetUserId(),
                IpAddress = FUserProvider.GetRequestIpAddress(),
                LikeCount = ARequest.AddToLikes > LLikesLimit ? LLikesLimit : ARequest.AddToLikes
            };
            
            await FDatabaseContext.ArticleLikes.AddAsync(LEntity, ACancellationToken);
        }

        private static void UpdateCurrentArticleLikes(bool AIsAnonymousUser, Domain.Entities.ArticleLikes AEntity, int ALikesToBeAdded)
        {
            var LLikesLimit = AIsAnonymousUser 
                ? Constants.Likes.LIKES_LIMIT_FOR_ANONYMOUS 
                : Constants.Likes.LIKES_LIMIT_FOR_USER;
            
            var LLikesSum = AEntity.LikeCount + ALikesToBeAdded;
            AEntity.LikeCount = LLikesSum > LLikesLimit ? LLikesLimit : LLikesSum;
        }
    }
}
