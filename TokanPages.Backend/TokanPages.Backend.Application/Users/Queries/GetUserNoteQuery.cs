using MediatR;

namespace TokanPages.Backend.Application.Users.Queries;

public class GetUserNoteQuery : IRequest<GetUserNoteQueryResult>
{
    public Guid UserNoteId { get; set; }
}