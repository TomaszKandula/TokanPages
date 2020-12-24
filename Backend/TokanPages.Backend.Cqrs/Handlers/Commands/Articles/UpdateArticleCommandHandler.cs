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

            var LCurrentArticle = await FDatabaseContext.Articles
                .AsNoTracking()
                .Where(Articles => Articles.Id == ARequest.Id).
                ToListAsync();

            if (!LCurrentArticle.Any())
            {
                throw new BusinessException(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS), ErrorCodes.ARTICLE_DOES_NOT_EXISTS);
            }

            if (!string.IsNullOrEmpty(ARequest.TextToUpload))
            {

                var LTextContent = await FFileUtility.SaveToFile("__upload", $"{ARequest.Id}.txt", ARequest.TextToUpload);
                var LTextUpload = await FAzureStorageService.UploadFile($"tokanpages\\content\\articles\\{ARequest.Id}", "text.html", LTextContent, "text/html", ACancellationToken);

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

                var LImageContent = await FFileUtility.SaveToFile("__upload", $"{ARequest.Id}.jpg", ARequest.ImageToUpload);

                var LImageUpload = await FAzureStorageService.UploadFile($"tokanpages\\content\\articles\\{ARequest.Id}", "image.jpeg", LImageContent, "image/jpeg", ACancellationToken);
                if (!LImageUpload.IsSucceeded)
                {
                    throw new BusinessException(nameof(ErrorCodes.CANNOT_SAVE_TO_AZURE_STORAGE), LImageUpload.ErrorDesc);
                }

            }

            var LUpdatedArticle = new Domain.Entities.Articles 
            {
                Title = ARequest.Title,
                Description = ARequest.Description,
                IsPublished = ARequest.IsPublished,
                Likes = ARequest.Likes,
                ReadCount = ARequest.ReadCount,
                UpdatedAt = DateTime.UtcNow
            };

            await FDatabaseContext.SaveChangesAsync(ACancellationToken);
            return await Task.FromResult(Unit.Value);
        
        }

    }

}
