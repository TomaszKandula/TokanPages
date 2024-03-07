using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Application.Articles.Models;

/// <summary>
/// Section definition.
/// </summary>
[ExcludeFromCodeCoverage]
public class ArticleSectionDto
{
    /// <summary>
    /// Id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Type.
    /// </summary>
    public string Type { get; set; } = "";
        
    /// <summary>
    /// Value.
    /// </summary>
    public dynamic? Value { get; set; }
        
    /// <summary>
    /// Property.
    /// </summary>
    public string Prop { get; set; } = "";
        
    /// <summary>
    /// Text.
    /// </summary>
    public string Text { get; set; } = "";
}