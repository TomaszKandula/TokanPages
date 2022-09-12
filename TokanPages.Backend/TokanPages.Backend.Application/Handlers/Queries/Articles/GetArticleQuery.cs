namespace TokanPages.Backend.Application.Handlers.Queries.Articles;

using System;
using MediatR;

public class GetArticleQuery : IRequest<GetArticleQueryResult>
{
    public Guid Id { get; set; }
}