namespace TokanPages.Users.Dto.Users;

/// <summary>
/// Use it when you want to update existing user note.
/// </summary>
public class UpdateUserNoteDto
{
    /// <summary>
    /// User note ID.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// New note.
    /// </summary>
    public string Note { get; set; } = "";
}