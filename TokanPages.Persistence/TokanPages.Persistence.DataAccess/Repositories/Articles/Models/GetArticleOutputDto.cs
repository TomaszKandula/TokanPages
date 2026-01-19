namespace TokanPages.Persistence.DataAccess.Repositories.Articles.Models;

public class GetArticleOutputDto : ArticleDataDto
{
    public int UserLikes { get; set; }

    public GetUserDto? Author { get; set; }

    public string[]? Tags { get; set; }

    public List<ArticleSectionDto>? Text { get; set; }
}