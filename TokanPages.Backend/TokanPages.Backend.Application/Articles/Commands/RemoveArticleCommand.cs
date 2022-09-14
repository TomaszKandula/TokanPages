using MediatR;

namespace TokanPages.Backend.Application.Articles.Commands;

public class RemoveArticleCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}