namespace TokanPages.Backend.Cqrs.Handlers.Commands.Articles
{
    using System;
    using MediatR;

    public class UpdateArticleVisibilityCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        
        public bool IsPublished { get; set; }
    }
}