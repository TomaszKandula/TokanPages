using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.Articles.Models;

[ExcludeFromCodeCoverage]
public class ArticleBaseDto
{
    public required Guid Id { get; init; }

    public required Guid UserId { get; init; }

    public required string Title { get; init; } = string.Empty;

    public required string Description { get; init; } = string.Empty;

    public required bool IsPublished { get; init; }

    public required DateTime CreatedAt { get; init; }

    public DateTime? UpdatedAt { get; init; }

    public required int ReadCount { get; init; }

    public required string LanguageIso { get; init; } = string.Empty;

    public required string CategoryName { get; init; } = string.Empty;

    public int? TotalLikes { get; init; }

    public int? UserLikes { get; init; }
}