using MediatR;

namespace TokanPages.Backend.Application.VideoProcessing;

public class VideoProcessingStatusQuery : IRequest<VideoProcessingStatusQueryResult>
{
    public Guid TicketId { get; set; }
}