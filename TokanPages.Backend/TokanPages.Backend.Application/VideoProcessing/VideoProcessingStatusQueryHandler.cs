using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace TokanPages.Backend.Application.VideoProcessing;

public class VideoProcessingStatusQueryHandler : RequestHandler<VideoProcessingStatusQuery, VideoProcessingStatusQueryResult>
{
    public VideoProcessingStatusQueryHandler(DatabaseContext databaseContext, ILoggerService loggerService) 
        : base(databaseContext, loggerService) { }

    public override async Task<VideoProcessingStatusQueryResult> Handle(VideoProcessingStatusQuery request, CancellationToken cancellationToken)
    {
        var videoData = await DatabaseContext.UploadedVideos
            .Where(videos => videos.TicketId == request.TicketId)
            .SingleOrDefaultAsync(cancellationToken);

        if (videoData is null)
            return new VideoProcessingStatusQueryResult();

        return new VideoProcessingStatusQueryResult
        {   
            Status = videoData.Status,
            VideoUri = videoData.TargetVideoUri,
            ThumbnailUri = videoData.TargetThumbnailUri
        };
    }
}