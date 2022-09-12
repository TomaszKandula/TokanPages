namespace TokanPages.Backend.Application.Articles.Queries;

using System.Collections.Generic;
using MediatR;

public class GetAllArticlesQuery : IRequest<List<GetAllArticlesQueryResult>>
{
    public bool IsPublished { get; set; }
}