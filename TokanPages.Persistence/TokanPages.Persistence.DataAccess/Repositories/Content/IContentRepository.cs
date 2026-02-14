using TokanPages.Backend.Domain.Enums;
using TokanPages.Persistence.DataAccess.Repositories.Content.Models;

namespace TokanPages.Persistence.DataAccess.Repositories.Content;

public interface IContentRepository
{
    Task<VideoUploadStatusDto?> GetVideoUploadStatus(Guid ticketId, VideoStatus? status = null);

    Task UpdateVideoUploadStatus(Guid ticketId, VideoStatus status);

    Task CreateVideoUpload(Guid userId, Guid ticketId, string sourceBlobUri, string targetVideoUri, string targetThumbnailUri);

    Task UpdateVideoUpload(UpdateVideoUploadDto data);

    Task<List<LanguageItemDto>?> GetContentLanguageList();
}