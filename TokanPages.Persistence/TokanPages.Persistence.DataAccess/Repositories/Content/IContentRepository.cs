using TokanPages.Persistence.DataAccess.Repositories.Content.Models;

namespace TokanPages.Persistence.DataAccess.Repositories.Content;

public interface IContentRepository
{
    Task<VideoUploadStatusDto?> GetVideoUploadStatus(Guid ticketId);
}