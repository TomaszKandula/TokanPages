namespace TokanPages.Backend.Application.Users.Queries;

public class GetUserNotesQueryResult
{
    public List<GetUserNoteQueryResult> Notes { get; set; } = new();
}