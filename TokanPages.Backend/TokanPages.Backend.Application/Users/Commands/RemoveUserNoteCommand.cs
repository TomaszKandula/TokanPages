using MediatR;

namespace TokanPages.Backend.Application.Users.Commands;

public class RemoveUserNoteCommand : IRequest<Unit>
{
    public Guid UserNoteId { get; set; }
}