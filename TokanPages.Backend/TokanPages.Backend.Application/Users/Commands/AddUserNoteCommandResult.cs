using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Backend.Application.Users.Commands;

public class AddUserNoteCommandResult
{
    public int CurrentNotes { get; set; }

    public UserNote Result { get; set; }
}