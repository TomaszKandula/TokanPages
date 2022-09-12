namespace TokanPages.Backend.Application.Articles.Queries;

using System;
using MediatR;

public class GetArticleQuery : IRequest<GetArticleQueryResult>
{
    public Guid Id { get; set; }
}