using MediatR;

namespace TokanPages.Backend.Application.Content.Assets.Queries;

public class GetVideoStatusQuery : IRequest<GetVideoStatusQueryResult>
{
    public Guid TicketId { get; set; }
}