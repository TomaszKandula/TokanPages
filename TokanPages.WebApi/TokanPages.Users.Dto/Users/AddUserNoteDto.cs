using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Users.Dto.Users;

/// <summary>
/// Use it when you want to add new user note.
/// </summary>
[ExcludeFromCodeCoverage]
public class AddUserNoteDto
{
    /// <summary>
    /// User note.
    /// </summary>
    public string Note { get; set; } = "";
}