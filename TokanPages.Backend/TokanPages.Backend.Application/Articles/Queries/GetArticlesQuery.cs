using MediatR;
using TokanPages.Backend.Core.Paging;

namespace TokanPages.Backend.Application.Articles.Queries;

public class GetArticlesQuery : PagingInfo, IRequest<GetAllArticlesQueryResult>
{
    public bool IsPublished { get; set; }

    public string? SearchTerm { get; set; }

    public Guid? CategoryId { get; set; }
}
