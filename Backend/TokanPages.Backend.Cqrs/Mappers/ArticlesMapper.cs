using TokanPages.Backend.Shared.Dto.Articles;
using TokanPages.Backend.Cqrs.Handlers.Queries.Articles;

namespace TokanPages.Backend.Cqrs.Mappers
{

    public static class ArticlesMapper
    {

        public static GetArticleCommand MapToGetArticleCommand(ArticleRequest AModel) 
        {
            return new GetArticleCommand 
            { 
                Id = AModel.Id
            };
        }

    }

}
