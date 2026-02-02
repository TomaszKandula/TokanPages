using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Persistence.DataAccess.Repositories.Content;

namespace TokanPages.Backend.Application.Content.Assets.Queries;

public class GetVideoStatusQueryHandler : RequestHandler<GetVideoStatusQuery, GetVideoStatusQueryResult>
{
    private readonly IContentRepository _contentRepository;

    public GetVideoStatusQueryHandler(OperationDbContext operationDbContext, ILoggerService loggerService, IContentRepository contentRepository) 
        : base(operationDbContext, loggerService) => _contentRepository = contentRepository;

    public override async Task<GetVideoStatusQueryResult> Handle(GetVideoStatusQuery request, CancellationToken cancellationToken)
    {
        var videoData = await _contentRepository.GetVideoUploadStatus(request.TicketId);
        if (videoData is null)
            return new GetVideoStatusQueryResult();

        return new GetVideoStatusQueryResult
        {   
            Status = videoData.Status,
            VideoUri = videoData.VideoUri,
            ThumbnailUri = videoData.ThumbnailUri
        };
    }
}