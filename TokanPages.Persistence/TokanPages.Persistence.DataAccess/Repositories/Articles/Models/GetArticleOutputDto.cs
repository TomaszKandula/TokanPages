namespace TokanPages.Persistence.DataAccess.Repositories.Articles.Models;

public class GetArticleOutputDto : ArticleDataDto
{
    public int UserLikes { get; init; }

    public GetUserDto? Author { get; init; }

    public string[]? Tags { get; init; }

    public List<ArticleSectionDto>? Text { get; init; }
}