using MediatR;

namespace TokanPages.Backend.Application.Articles.Queries;

public class GetArticlesQuery : IRequest<List<GetArticlesQueryResult>>
{
    public bool IsPublished { get; set; }
}