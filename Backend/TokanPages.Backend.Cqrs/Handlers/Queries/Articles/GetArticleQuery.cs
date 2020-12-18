using System;
using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Queries.Articles
{

    public class GetArticleQuery : IRequest<Domain.Entities.Articles>
    {
        public Guid Id { get; set; }
    }

}
