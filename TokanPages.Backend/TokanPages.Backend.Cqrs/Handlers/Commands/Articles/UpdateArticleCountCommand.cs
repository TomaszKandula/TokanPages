using System;
using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Articles
{
    public class UpdateArticleCountCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}