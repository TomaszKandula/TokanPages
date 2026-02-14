using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Repositories.Content;

namespace TokanPages.Backend.Application.Content.Assets.Queries;

public class GetVideoStatusQueryHandler : RequestHandler<GetVideoStatusQuery, GetVideoStatusQueryResult>
{
    private readonly IContentRepository _contentRepository;

    public GetVideoStatusQueryHandler(ILoggerService loggerService, IContentRepository contentRepository) 
        : base(loggerService) => _contentRepository = contentRepository;

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