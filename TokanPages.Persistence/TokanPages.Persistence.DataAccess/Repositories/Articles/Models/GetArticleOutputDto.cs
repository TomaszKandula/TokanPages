using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.Articles.Models;

[ExcludeFromCodeCoverage]
public class GetArticleOutputDto : ArticleDataDto
{
    public required int UserLikes { get; init; }

    public GetUserDto? Author { get; init; }

    public string[]? Tags { get; init; }

    public List<ArticleSectionDto>? Text { get; init; }
}