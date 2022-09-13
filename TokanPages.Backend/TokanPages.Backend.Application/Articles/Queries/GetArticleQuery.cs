using System;
using MediatR;

namespace TokanPages.Backend.Application.Articles.Queries;

public class GetArticleQuery : IRequest<GetArticleQueryResult>
{
    public Guid Id { get; set; }
}