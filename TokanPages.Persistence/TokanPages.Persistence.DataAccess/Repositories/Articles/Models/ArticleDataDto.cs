using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.Articles.Models;

[ExcludeFromCodeCoverage]
public class ArticleDataDto
{
    public Guid Id { get; init; }

    public string CategoryName { get; init; } = string.Empty;

    public string Title { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

    public bool IsPublished { get; init; }

    public int ReadCount { get; init; }

    public int TotalLikes { get; init; }

    public DateTime CreatedAt { get; init; }

    public DateTime? UpdatedAt { get; init; }

    public string? LanguageIso { get; init; }

    public int? CountOver { get; init; }
}