using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.Articles.Models;

[ExcludeFromCodeCoverage]
public class ArticleSectionDto
{
    public required Guid Id { get; init; }

    public required string Type { get; init; } = string.Empty;
        
    public dynamic? Value { get; init; }
        
    public required string Prop { get; init; } = string.Empty;
        
    public required string Text { get; init; } = string.Empty;
}