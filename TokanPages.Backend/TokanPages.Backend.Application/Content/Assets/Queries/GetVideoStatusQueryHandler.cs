using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using TokanPages.Persistence.Database.Contexts;

namespace TokanPages.Backend.Application.Content.Assets.Queries;

public class GetVideoStatusQueryHandler : RequestHandler<GetVideoStatusQuery, GetVideoStatusQueryResult>
{
    public GetVideoStatusQueryHandler(OperationDbContext operationDbContext, ILoggerService loggerService) 
        : base(operationDbContext, loggerService) { }

    public override async Task<GetVideoStatusQueryResult> Handle(GetVideoStatusQuery request, CancellationToken cancellationToken)
    {
        var videoData = await OperationDbContext.UploadedVideos
            .Where(video => video.TicketId == request.TicketId)
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