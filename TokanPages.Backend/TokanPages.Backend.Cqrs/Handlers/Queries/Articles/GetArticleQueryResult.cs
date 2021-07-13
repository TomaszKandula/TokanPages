namespace TokanPages.Backend.Cqrs.Handlers.Queries.Articles
{
    using TokanPages.Backend.Shared.Dto.Users;

    public class GetArticleQueryResult : GetAllArticlesQueryResult
    {
        public int LikeCount { get; set; }
        
        public int UserLikes { get; set; }

        public GetUserDto Author { get; set; }
    }
}