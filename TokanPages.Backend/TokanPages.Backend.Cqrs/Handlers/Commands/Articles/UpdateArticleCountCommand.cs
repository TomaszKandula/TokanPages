namespace TokanPages.Backend.Cqrs.Handlers.Commands.Articles;

using System;
using MediatR;

public class UpdateArticleCountCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}