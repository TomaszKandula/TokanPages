using MediatR;

namespace TokanPages.Backend.Application.Users.Commands;

public class AddUserNoteCommand : IRequest<Unit>
{
    public string Note { get; set; } = "";
}