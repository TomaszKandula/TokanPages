using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.Articles.Models;

/// <summary>
/// Section definition.
/// </summary>
[ExcludeFromCodeCoverage]
public class ArticleSectionDto
{
    /// <summary>
    /// Id.
    /// </summary>
    public required Guid Id { get; init; }

    /// <summary>
    /// Type.
    /// </summary>
    public required string Type { get; init; } = string.Empty;
        
    /// <summary>
    /// Value.
    /// </summary>
    public dynamic? Value { get; init; }
        
    /// <summary>
    /// Property.
    /// </summary>
    public required string Prop { get; init; } = string.Empty;
        
    /// <summary>
    /// Text.
    /// </summary>
    public required string Text { get; init; } = string.Empty;
}