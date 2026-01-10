using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Users.Dto.Users;

/// <summary>
/// Use it when you want to remove user file.
/// </summary>
[ExcludeFromCodeCoverage]
public class RemoveUserFileDto
{
    /// <summary>
    /// User file type (image, audio, video, document, application).
    /// </summary>
    public UserFileToUpdate Type { get; set; }

    /// <summary>
    /// File name.
    /// </summary>
    public string UniqueBlobName { get; set; } = "";
}