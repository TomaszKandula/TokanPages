using System;
using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Articles
{
    public class UpdateArticleLikesCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        
        public int AddToLikes { get; set; }
    }
}