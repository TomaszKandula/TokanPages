using System.Diagnostics.CodeAnalysis;

namespace TokanPages.WebApi.Dto.Users;

/// <summary>
/// Use it when you want to remove user asset (image/video).
/// </summary>
[ExcludeFromCodeCoverage]
public class RemoveUserMediaDto
{
    /// <summary>
    /// Unique blob name.
    /// </summary>
    public string UniqueBlobName { get; set; } = "";
}