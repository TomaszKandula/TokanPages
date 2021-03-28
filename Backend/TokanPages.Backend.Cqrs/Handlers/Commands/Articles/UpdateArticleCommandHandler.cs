using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Shared;
using TokanPages.Backend.Storage;
using TokanPages.Backend.Database;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Core.Services.FileUtility;
using TokanPages.Backend.Cqrs.Services.UserProvider;
using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Articles
{
    public class UpdateArticleCommandHandler : TemplateHandler<UpdateArticleCommand, Unit>
    {
        private readonly DatabaseContext FDatabaseContext;
        private readonly IAzureStorageService FAzureStorageService;
        private readonly IFileUtility FFileUtility;
        private readonly IUserProvider FUserProvider;

        public UpdateArticleCommandHandler(DatabaseContext ADatabaseContext,
            IAzureStorageService AAzureStorageService, IFileUtility AFileUtility,
            IUserProvider AUserProvider)
        {
            FDatabaseContext = ADatabaseContext;
            FAzureStorageService = AAzureStorageService;
            FFileUtility = AFileUtility;
            FUserProvider = AUserProvider;
        }

        public override async Task<Unit> Handle(UpdateArticleCommand ARequest, CancellationToken ACancellationToken)
        {
            var LArticles = await FDatabaseContext.Articles
                .Where(Articles => Articles.Id == ARequest.Id)
                .ToListAsync(ACancellationToken);

            if (!LArticles.Any())
            {
                throw new BusinessException(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS), ErrorCodes.ARTICLE_DOES_NOT_EXISTS);
            }

            if (!string.IsNullOrEmpty(ARequest.TextToUpload))
            {
                var LTextContent = await FFileUtility
                    .SaveToFile("__upload", $"{ARequest.Id}.json", ARequest.TextToUpload);
                var LTextUpload = await FAzureStorageService
                    .UploadFile($"content\\articles\\{ARequest.Id.ToString().ToLower()}", "text.json", LTextContent, "application/json", ACancellationToken);

                if (!LTextUpload.IsSucceeded)
                {
                    throw new BusinessException(nameof(ErrorCodes.CANNOT_SAVE_TO_AZURE_STORAGE), LTextUpload.ErrorDesc);
                }
            }

            if (!string.IsNullOrEmpty(ARequest.ImageToUpload))
            {
                var LImageBase64Check = FFileUtility.IsBase64String(ARequest.ImageToUpload);
                if (!LImageBase64Check)
                {
                    throw new BusinessException(nameof(ErrorCodes.INVALID_BASE64), ErrorCodes.INVALID_BASE64);
                }

                var LImageContent = await FFileUtility
                    .SaveToFile("__upload", $"{ARequest.Id}.jpg", ARequest.ImageToUpload);
                var LImageUpload = await FAzureStorageService
                    .UploadFile($"content\\articles\\{ARequest.Id.ToString().ToLower()}", "image.jpeg", LImageContent, "image/jpeg", ACancellationToken);

                if (!LImageUpload.IsSucceeded)
                {
                    throw new BusinessException(nameof(ErrorCodes.CANNOT_SAVE_TO_AZURE_STORAGE), LImageUpload.ErrorDesc);
                }
            }

            var LCurrentArticle = LArticles.First();

            LCurrentArticle.Title = ARequest.Title ?? LCurrentArticle.Title;
            LCurrentArticle.Description = ARequest.Description ?? LCurrentArticle.Description;
            LCurrentArticle.IsPublished = ARequest.IsPublished ?? LCurrentArticle.IsPublished;

            LCurrentArticle.ReadCount = (ARequest.UpReadCount.HasValue && ARequest.UpReadCount == true) 
                ? LCurrentArticle.ReadCount + 1 
                : LCurrentArticle.ReadCount;

            LCurrentArticle.UpdatedAt = (ARequest.Title != null && ARequest.Description != null)
                ? DateTime.UtcNow
                : LCurrentArticle.UpdatedAt;

            var IsAnonymousUser = FUserProvider.GetUserId() == null;
            
            var LArticleLikes = await FDatabaseContext.ArticleLikes
                .Where(Likes => Likes.ArticleId == ARequest.Id)
                .WhereIfElse(IsAnonymousUser,
                    Likes => Likes.IpAddress == FUserProvider.GetRequestIpAddress(),
                    Likes => Likes.UserId == FUserProvider.GetUserId())
                .ToListAsync(ACancellationToken);

            if (!LArticleLikes.Any())
            {
                AddNewArticleLikes(IsAnonymousUser, ARequest);
            }
            else
            {
                UpdateCurrentArticleLikes(IsAnonymousUser, LArticleLikes.First(), ARequest.AddToLikes);
            }

            await FDatabaseContext.SaveChangesAsync(ACancellationToken);
            return await Task.FromResult(Unit.Value);
        }

        private void AddNewArticleLikes(bool AIsAnonymousUser, UpdateArticleCommand ARequest)
        {
            var LLikesLimit = AIsAnonymousUser 
                ? Constants.Likes.LikesLimitForAnonym 
                : Constants.Likes.LikesLimitForUser;
            
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
                ? Constants.Likes.LikesLimitForAnonym 
                : Constants.Likes.LikesLimitForUser;
            
            var LLikesSum = AEntity.LikeCount + ALikesToBeAdded;
            AEntity.LikeCount = LLikesSum > LLikesLimit ? LLikesLimit : LLikesSum;
        }
    }
}
