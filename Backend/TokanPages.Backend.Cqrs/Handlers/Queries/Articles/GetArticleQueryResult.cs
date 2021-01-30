using TokanPages.Backend.Shared.Dto.Users;

namespace TokanPages.Backend.Cqrs.Handlers.Queries.Articles
{
    public class GetArticleQueryResult : GetAllArticlesQueryResult
    {
        public int LikeCount { get; set; }
        public GetUserDto Author { get; set; }
    }
}
