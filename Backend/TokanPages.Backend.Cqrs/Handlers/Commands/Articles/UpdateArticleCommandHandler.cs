using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Storage;
using TokanPages.Backend.Database;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Core.Services.FileUtility;
using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Articles
{   
    public class UpdateArticleCommandHandler : TemplateHandler<UpdateArticleCommand, Unit>
    {
        private readonly DatabaseContext FDatabaseContext;
        private readonly IAzureStorageService FAzureStorageService;
        private readonly IFileUtility FFileUtility;

        public UpdateArticleCommandHandler(DatabaseContext ADatabaseContext, 
            IAzureStorageService AAzureStorageService, IFileUtility AFileUtility) 
        {
            FDatabaseContext = ADatabaseContext;
            FAzureStorageService = AAzureStorageService;
            FFileUtility = AFileUtility;
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
            LCurrentArticle.Likes = LCurrentArticle.Likes + ARequest.AddToLikes;

            if (ARequest.UpReadCount.HasValue && ARequest.UpReadCount == true) 
            {
                LCurrentArticle.ReadCount++;
            }

            if (ARequest.Title != null && ARequest.Description != null)
            {
                LCurrentArticle.UpdatedAt = DateTime.UtcNow;
            }

            FDatabaseContext.Articles.Attach(LCurrentArticle).State = EntityState.Modified;
            await FDatabaseContext.SaveChangesAsync(ACancellationToken);
            return await Task.FromResult(Unit.Value);       
        }
    }
}
