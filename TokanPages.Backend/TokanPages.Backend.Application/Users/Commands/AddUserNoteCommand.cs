using MediatR;

namespace TokanPages.Backend.Application.Users.Commands;

public class AddUserNoteCommand : IRequest<AddUserNoteCommandResult>
{
    public string Note { get; set; } = "";
}