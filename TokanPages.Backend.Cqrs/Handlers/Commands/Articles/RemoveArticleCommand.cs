using System;
using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Articles
{
    public class RemoveArticleCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
