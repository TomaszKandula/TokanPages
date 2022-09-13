using System.Collections.Generic;
using MediatR;

namespace TokanPages.Backend.Application.Articles.Queries;

public class GetAllArticlesQuery : IRequest<List<GetAllArticlesQueryResult>>
{
    public bool IsPublished { get; set; }
}