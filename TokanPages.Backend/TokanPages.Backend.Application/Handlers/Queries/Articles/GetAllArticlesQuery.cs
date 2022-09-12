namespace TokanPages.Backend.Application.Handlers.Queries.Articles;

using System.Collections.Generic;
using MediatR;

public class GetAllArticlesQuery : IRequest<List<GetAllArticlesQueryResult>>
{
    public bool IsPublished { get; set; }
}