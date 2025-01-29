using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Backend.Application.Users.Commands;

public class AddUserNoteCommandResult
{
    public Guid Id { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public int CurrentNotes { get; set; }

    public UserNote Result { get; set; }
}