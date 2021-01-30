using System.Collections.Generic;
using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Queries.Articles
{
    public class GetAllArticlesQuery : IRequest<IEnumerable<GetAllArticlesQueryResult>>
    {
        public bool IsPublished { get; set; }
    }
}
