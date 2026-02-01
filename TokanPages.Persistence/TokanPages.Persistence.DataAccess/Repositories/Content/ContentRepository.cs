using Microsoft.Extensions.Options;
using TokanPages.Backend.Configuration.Options;
using TokanPages.Backend.Domain.Entities;
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
}