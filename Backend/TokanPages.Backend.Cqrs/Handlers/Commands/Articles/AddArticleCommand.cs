using System;
using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Articles
{

    public class AddArticleCommand : IRequest<Unit>
    {
        public string Title { get; set; }
        public string Desc { get; set; }
        public string Status { get; set; }
        public string Text { get; set; }
    }

}
