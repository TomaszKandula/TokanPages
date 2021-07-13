namespace TokanPages.Backend.Cqrs.Handlers.Queries.Articles
{
    using System.Collections.Generic;
    using MediatR;

    public class GetAllArticlesQuery : IRequest<IEnumerable<GetAllArticlesQueryResult>>
    {
        public bool IsPublished { get; set; }
    }
}