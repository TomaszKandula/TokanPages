using System.Diagnostics.CodeAnalysis;

namespace TokanPages.WebApi.Dto.Articles;

/// <summary>
/// Use it when you want to update likes.
/// </summary>
[ExcludeFromCodeCoverage]
public class UpdateArticleLikesDto
{
    /// <summary>
    /// Identification.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Like number to be added.
    /// </summary>
    public int AddToLikes { get; set; }
}