using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.Articles.Models;

[ExcludeFromCodeCoverage]
public class ArticleDataDto
{
    public required Guid Id { get; init; }

    public required string CategoryName { get; init; }

    public required string Title { get; init; }

    public required string Description { get; init; }

    public required bool IsPublished { get; init; }

    public required int ReadCount { get; init; }

    public required int TotalLikes { get; init; }

    public required DateTime CreatedAt { get; init; }

    public DateTime? UpdatedAt { get; init; }

    public string? LanguageIso { get; init; }

    public int? CountOver { get; init; }
}