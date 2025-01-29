using MediatR;

namespace TokanPages.Backend.Application.Users.Commands;

public class UpdateUserNoteCommand : IRequest<Unit>
{
    public Guid Id { get; set; }

    public string Note { get; set; } = "";
}