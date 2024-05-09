using MediatR;

namespace TokanPages.Backend.Application.Articles.Queries;

public class GetArticleQuery : IRequest<GetArticleQueryResult>
{
    public Guid? Id { get; set; }
    
    public string? Title { get; set; }
}