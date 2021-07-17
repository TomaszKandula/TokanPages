namespace TokanPages.Backend.Cqrs.Handlers.Commands.Articles
{
    using System;
    using MediatR;

    public class UpdateArticleLikesCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        
        public int AddToLikes { get; set; }
    }
}