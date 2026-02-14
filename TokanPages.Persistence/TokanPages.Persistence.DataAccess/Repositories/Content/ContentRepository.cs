using Microsoft.Extensions.Options;
using TokanPages.Backend.Domain.Entities;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Backend.Shared.Options;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Abstractions;
using TokanPages.Persistence.DataAccess.Repositories.Content.Models;

namespace TokanPages.Persistence.DataAccess.Repositories.Content;

public class ContentRepository : RepositoryBase, IContentRepository
{
    private readonly IDateTimeService _dateTimeService;

    public ContentRepository(IDbOperations dbOperations, IOptions<AppSettingsModel> appSettings, IDateTimeService dateTimeService) 
        : base(dbOperations,  appSettings) => _dateTimeService = dateTimeService;

    public async Task<VideoUploadStatusDto?> GetVideoUploadStatus(Guid ticketId, VideoStatus? status = null)
    {
        dynamic filterBy = status == null ? new { TicketId = ticketId } : new { TicketId = ticketId, Status = status };
        var data = await DbOperations.Retrieve<UploadedVideo>(filterBy) as IEnumerable<UploadedVideo>;

        var result = data?.SingleOrDefault();
        if (result is null)
            return null;

        return new VideoUploadStatusDto
        {
            Status = result.Status,
            VideoUri = result.TargetVideoUri,
            ThumbnailUri = result.TargetThumbnailUri,
            SourceBlobUri = result.SourceBlobUri,
            TargetVideoUri = result.TargetVideoUri,
            TargetThumbnailUri = result.TargetThumbnailUri,
        };
    }

    public async Task UpdateVideoUploadStatus(Guid ticketId, VideoStatus status)
    {
        var updateBy = new { Status = status };
        var filterBy = new { TicketId = ticketId };
        await DbOperations.Update<UploadedVideo>(updateBy, filterBy);
    }

    public async Task CreateVideoUpload(Guid userId, Guid ticketId, string sourceBlobUri, string targetVideoUri, string targetThumbnailUri)
    {
        var entity = new UploadedVideo
        {
            Id = Guid.NewGuid(),
            TicketId = ticketId,
            SourceBlobUri = sourceBlobUri,
            TargetVideoUri = targetVideoUri,
            TargetThumbnailUri = targetThumbnailUri,
            Status = VideoStatus.New,
            CreatedAt = _dateTimeService.Now,
            CreatedBy = userId,
            IsSourceDeleted = false,
            InputSizeInBytes = 0,
            OutputSizeInBytes = 0
        };

        await DbOperations.Insert(entity);            
    }

    public async Task UpdateVideoUpload(UpdateVideoUploadDto data)
    {
        var updateBy = new
        {
            Status = data.Status,
            IsSourceDeleted = data.IsSourceDeleted,
            ProcessingWarning = data.ProcessingWarning,
            InputSizeInBytes = data.InputSizeInBytes,
            OutputSizeInBytes = data.OutputSizeInBytes,
            ModifiedAt = _dateTimeService.Now,
        };

        var filterBy = new
        {
            TicketId = data.TicketId
        };

        await DbOperations.Update<UploadedVideo>(updateBy, filterBy);
    }

    public async Task<List<LanguageItemDto>?> GetContentLanguageList()
    {
        var data = await DbOperations.Retrieve<Language>(orderBy: new { SortOrder = "ASC" });
        var result = data.ToList(); 
        if (result.Count == 0)
            return null;

        var languages = new List<LanguageItemDto>();
        foreach (var item in result)
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