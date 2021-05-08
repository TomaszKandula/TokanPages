using System;
using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Queries.Articles
{
    public class GetArticleQuery : IRequest<GetArticleQueryResult>
    {
        public Guid Id { get; set; }
    }
}
