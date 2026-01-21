using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.Articles.Models;

[ExcludeFromCodeCoverage]
public class ArticleBaseDto
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public bool IsPublished { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int ReadCount { get; set; }

    public string LanguageIso { get; set; } = string.Empty;

    public string CategoryName { get; set; } = string.Empty;

    public int? TotalLikes { get; set; }

    public int? UserLikes { get; set; }
}