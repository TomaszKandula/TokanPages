using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Shared;
using TokanPages.Backend.Database;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Storage.AzureStorage;
using TokanPages.Backend.Core.Services.FileUtility;
using TokanPages.Backend.Cqrs.Services.UserProvider;
using TokanPages.Backend.Core.Services.DateTimeService;
using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Articles
{
    public class UpdateArticleCommandHandler : TemplateHandler<UpdateArticleCommand, Unit>
    {
        private readonly DatabaseContext FDatabaseContext;
        private readonly IAzureStorageService FAzureStorageService;
        private readonly IFileUtilityService FFileUtilityService;
        private readonly IUserProvider FUserProvider;
        private readonly IDateTimeService FDateTimeService;

        public UpdateArticleCommandHandler(DatabaseContext ADatabaseContext,
            IAzureStorageService AAzureStorageService, IFileUtilityService AFileUtilityService,
            IUserProvider AUserProvider, IDateTimeService ADateTimeService)
        {
            FDatabaseContext = ADatabaseContext;
            FAzureStorageService = AAzureStorageService;
            FFileUtilityService = AFileUtilityService;
            FUserProvider = AUserProvider;
            FDateTimeService = ADateTimeService;
        }

        public override async Task<Unit> Handle(UpdateArticleCommand ARequest, CancellationToken ACancellationToken)
        {
            var LArticles = await FDatabaseContext.Articles
                .Where(AArticles => AArticles.Id == ARequest.Id)
                .ToListAsync(ACancellationToken);

            if (!LArticles.Any())
                throw new BusinessException(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS), ErrorCodes.ARTICLE_DOES_NOT_EXISTS);

            if (!string.IsNullOrEmpty(ARequest.TextToUpload))
                await UploadText(ARequest, ACancellationToken);

            if (!string.IsNullOrEmpty(ARequest.ImageToUpload))
                await UploadImage(ARequest, ACancellationToken);

            var LCurrentArticle = LArticles.First();

            LCurrentArticle.Title = ARequest.Title ?? LCurrentArticle.Title;
            LCurrentArticle.Description = ARequest.Description ?? LCurrentArticle.Description;
            LCurrentArticle.IsPublished = ARequest.IsPublished ?? LCurrentArticle.IsPublished;

            LCurrentArticle.ReadCount = ARequest.UpReadCount is true
                ? LCurrentArticle.ReadCount + 1 
                : LCurrentArticle.ReadCount;

            LCurrentArticle.UpdatedAt = ARequest.Title != null && ARequest.Description != null
                ? FDateTimeService.Now
                : LCurrentArticle.UpdatedAt;

            var LIsAnonymousUser = FUserProvider.GetUserId() == null;
            
            var LArticleLikes = await FDatabaseContext.ArticleLikes
                .Where(ALikes => ALikes.ArticleId == ARequest.Id)
                .WhereIfElse(LIsAnonymousUser,
                    ALikes => ALikes.IpAddress == FUserProvider.GetRequestIpAddress(),
                    ALikes => ALikes.UserId == FUserProvider.GetUserId())
                .ToListAsync(ACancellationToken);

            if (!LArticleLikes.Any())
            {
                AddNewArticleLikes(LIsAnonymousUser, ARequest);
            }
            else
            {
                UpdateCurrentArticleLikes(LIsAnonymousUser, LArticleLikes.First(), ARequest.AddToLikes);
            }

            await FDatabaseContext.SaveChangesAsync(ACancellationToken);
            return await Task.FromResult(Unit.Value);
        }

        private async Task UploadImage(UpdateArticleCommand ARequest, CancellationToken ACancellationToken)
        {
            if (!ARequest.ImageToUpload.IsBase64String())
                throw new BusinessException(nameof(ErrorCodes.INVALID_BASE64), ErrorCodes.INVALID_BASE64);

            var LImageContent = await FFileUtilityService
                .SaveToFile(
                    "__upload", 
                    $"{ARequest.Id}.jpg", 
                    ARequest.ImageToUpload);
            
            var LImageUpload = await FAzureStorageService
                .UploadFile(
                    $"content\\articles\\{ARequest.Id.ToString().ToLower()}", 
                    "image.jpeg", 
                    LImageContent, 
                    "image/jpeg",
                    ACancellationToken);

            if (!LImageUpload.IsSucceeded)
                throw new BusinessException(nameof(ErrorCodes.CANNOT_SAVE_TO_AZURE_STORAGE), LImageUpload.ErrorDesc);
        }

        private async Task UploadText(UpdateArticleCommand ARequest, CancellationToken ACancellationToken)
        {
            var LTextContent = await FFileUtilityService
                .SaveToFile(
                    "__upload", 
                    $"{ARequest.Id}.json", 
                    ARequest.TextToUpload);
            
            var LTextUpload = await FAzureStorageService
                .UploadFile(
                    $"content\\articles\\{ARequest.Id.ToString().ToLower()}", 
                    "text.json", 
                    LTextContent,
                    "application/json", 
                    ACancellationToken);

            if (!LTextUpload.IsSucceeded)
                throw new BusinessException(nameof(ErrorCodes.CANNOT_SAVE_TO_AZURE_STORAGE), LTextUpload.ErrorDesc);
        }

        private void AddNewArticleLikes(bool AIsAnonymousUser, UpdateArticleCommand ARequest)
        {
            var LLikesLimit = AIsAnonymousUser 
                ? Constants.Likes.LIKES_LIMIT_FOR_ANONYMOUS 
                : Constants.Likes.LIKES_LIMIT_FOR_USER;
            
            var LEntity = new Domain.Entities.ArticleLikes
            {
                Id = Guid.NewGuid(),
                ArticleId = ARequest.Id,
                UserId = FUserProvider.GetUserId(),
                IpAddress = FUserProvider.GetRequestIpAddress(),
                LikeCount = ARequest.AddToLikes > LLikesLimit ? LLikesLimit : ARequest.AddToLikes
            };
            
            FDatabaseContext.ArticleLikes.Add(LEntity);
        }

        private void UpdateCurrentArticleLikes(bool AIsAnonymousUser, Domain.Entities.ArticleLikes AEntity, int ALikesToBeAdded)
        {
            var LLikesLimit = AIsAnonymousUser 
                ? Constants.Likes.LIKES_LIMIT_FOR_ANONYMOUS 
                : Constants.Likes.LIKES_LIMIT_FOR_USER;
            
            var LLikesSum = AEntity.LikeCount + ALikesToBeAdded;
            AEntity.LikeCount = LLikesSum > LLikesLimit ? LLikesLimit : LLikesSum;
        }
    }
}
