using MediatR;

namespace TokanPages.Backend.Application.Articles.Commands;

public class UpdateArticleCountCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}