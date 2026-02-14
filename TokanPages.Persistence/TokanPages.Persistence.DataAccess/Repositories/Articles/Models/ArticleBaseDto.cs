using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.Articles.Models;

[ExcludeFromCodeCoverage]
public class ArticleBaseDto
{
    public Guid Id { get; init; }

    public Guid UserId { get; init; }

    public string Title { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

    public bool IsPublished { get; init; }

    public DateTime CreatedAt { get; init; }

    public DateTime? UpdatedAt { get; init; }

    public int ReadCount { get; init; }

    public string LanguageIso { get; init; } = string.Empty;

    public string CategoryName { get; init; } = string.Empty;

    public int? TotalLikes { get; init; }

    public int? UserLikes { get; init; }
}