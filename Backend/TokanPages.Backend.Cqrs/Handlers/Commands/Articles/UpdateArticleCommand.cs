using System;
using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Articles
{

    public class UpdateArticleCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }
        public bool IsPublished { get; set; }
        public int Likes { get; set; }
        public int ReadCount { get; set; }
    }

}
