using System;
using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Articles
{
    public class UpdateArticleVisibilityCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        
        public bool IsPublished { get; set; }
    }
}