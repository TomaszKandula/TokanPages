using MediatR;

namespace TokanPages.Backend.Application.Assets.Queries;

public class GetVideoStatusQuery : IRequest<GetVideoStatusQueryResult>
{
    public Guid TicketId { get; set; }
}