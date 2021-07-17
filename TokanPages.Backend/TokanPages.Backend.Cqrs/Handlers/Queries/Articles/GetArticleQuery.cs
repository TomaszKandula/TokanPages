namespace TokanPages.Backend.Cqrs.Handlers.Queries.Articles
{
    using System;
    using MediatR;

    public class GetArticleQuery : IRequest<GetArticleQueryResult>
    {
        public Guid Id { get; set; }
    }
}