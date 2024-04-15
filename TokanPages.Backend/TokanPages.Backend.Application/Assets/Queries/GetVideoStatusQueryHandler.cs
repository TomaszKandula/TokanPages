using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace TokanPages.Backend.Application.Assets.Queries;

public class GetVideoStatusQueryHandler : RequestHandler<GetVideoStatusQuery, GetVideoStatusQueryResult>
{
    public GetVideoStatusQueryHandler(DatabaseContext databaseContext, ILoggerService loggerService) 
        : base(databaseContext, loggerService) { }

    public override async Task<GetVideoStatusQueryResult> Handle(GetVideoStatusQuery request, CancellationToken cancellationToken)
    {
        var videoData = await DatabaseContext.UploadedVideos
            .Where(videos => videos.TicketId == request.TicketId)
            .SingleOrDefaultAsync(cancellationToken);

        if (videoData is null)
            return new GetVideoStatusQueryResult();

        return new GetVideoStatusQueryResult
        {   
            Status = videoData.Status,
            VideoUri = videoData.TargetVideoUri,
            ThumbnailUri = videoData.TargetThumbnailUri
        };
    }
}