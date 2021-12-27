namespace TokanPages.Backend.Cqrs.Handlers.Commands.Articles;

using System;
using MediatR;

public class RemoveArticleCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}