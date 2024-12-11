using MediatR;

namespace TokanPages.Backend.Application.Articles.Queries;

public class GetArticleInfoQuery : IRequest<GetArticleInfoQueryResult>
{
    public Guid? Id { get; set; }
}