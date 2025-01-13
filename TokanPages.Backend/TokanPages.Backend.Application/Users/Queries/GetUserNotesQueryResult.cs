namespace TokanPages.Backend.Application.Users.Queries;

public class GetUserNotesQueryResult
{
    public List<GetUserNoteQueryResult> UserNotes { get; set; } = new();
}