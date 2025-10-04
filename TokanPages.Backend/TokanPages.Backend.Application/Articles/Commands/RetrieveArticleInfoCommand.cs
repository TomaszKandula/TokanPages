using MediatR;

namespace TokanPages.Backend.Application.Articles.Commands;

public class RetrieveArticleInfoCommand : IRequest<RetrieveArticleInfoCommandResult>
{
    public List<Guid> ArticleIds { get; set; } = new();
}