namespace TokanPages.Backend.Application.Users.Queries;

public class GetAllUserNotesQueryResult
{
    public List<GetUserNoteQueryResult> UserNotes { get; set; } = new();
}