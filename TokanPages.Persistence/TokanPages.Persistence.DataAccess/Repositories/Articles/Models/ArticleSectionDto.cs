using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.Articles.Models;

[ExcludeFromCodeCoverage]
public class ArticleSectionDto
{
    public required Guid Id { get; init; }

    public required string Type { get; init; }
        
    public dynamic? Value { get; init; }
        
    public required string Prop { get; init; }
        
    public required string Text { get; init; }
}