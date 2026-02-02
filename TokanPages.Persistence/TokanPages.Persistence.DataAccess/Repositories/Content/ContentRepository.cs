using Microsoft.Extensions.Options;
using TokanPages.Backend.Configuration.Options;
using TokanPages.Backend.Domain.Entities;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Persistence.DataAccess.Abstractions;
using TokanPages.Persistence.DataAccess.Repositories.Content.Models;

namespace TokanPages.Persistence.DataAccess.Repositories.Content;

public class ContentRepository : RepositoryPattern, IContentRepository
{
    public ContentRepository(IDbOperations dbOperations, IOptions<AppSettingsModel> appSettings) 
        : base(dbOperations,  appSettings) { }

    public async Task<VideoUploadStatusDto?> GetVideoUploadStatus(Guid ticketId)
    {
        var filterBy = new { TicketId = ticketId };
        var data = (await DbOperations.Retrieve<UploadedVideo>(filterBy)).SingleOrDefault();
        if (data is null)
            return null;

        return new VideoUploadStatusDto
        {
            Status = data.Status,
            VideoUri = data.TargetVideoUri,
            ThumbnailUri = data.TargetThumbnailUri
        };
    }

    public async Task<bool> UploadVideo(Guid userId, Guid ticketId, string sourceBlobUri, string targetVideoUri, string targetThumbnailUri, DateTime createdAt)
    {
        try
        {
            var entity = new UploadedVideo
            {
                Id = Guid.NewGuid(),
                TicketId = ticketId,
                SourceBlobUri = sourceBlobUri,
                TargetVideoUri = targetVideoUri,
                TargetThumbnailUri = targetThumbnailUri,
                Status = VideoStatus.New,
                CreatedAt = createdAt,
                CreatedBy = userId,
                IsSourceDeleted = false
            };

            await DbOperations.Insert(entity);            
        }
        catch
        {
            return false;
        }

        return true;
    }

    public async Task<List<LanguageItemDto>?> GetContentLanguageList()
    {
        var data = (await DbOperations.Retrieve<Language>(new { })).ToList();
        if (data.Count == 0)
            return null;

        var languages = new List<LanguageItemDto>();
        foreach (var item in data)
        {
            languages.Add(new LanguageItemDto
            {
                Id = item.LangId,
                Iso = item.HrefLang,
                Name = item.Name,
                IsDefault = item.IsDefault
            });
        }

        return languages;
    }
}