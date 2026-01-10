using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Users.Dto.Users;

/// <summary>
/// Use it when you want to remove existing user note.
/// </summary>
[ExcludeFromCodeCoverage]
public class RemoveUserNoteDto
{
    /// <summary>
    /// User note ID.
    /// </summary>
    public Guid Id { get; set; }
}