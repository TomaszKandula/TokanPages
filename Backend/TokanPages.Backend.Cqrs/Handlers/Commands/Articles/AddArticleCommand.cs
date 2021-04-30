﻿using System;
using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Articles
{
    public class AddArticleCommand : IRequest<Guid>
    {
        public string Title { get; set; }

        public string Description { get; set; }
        
        public string TextToUpload { get; set; }
        
        public string ImageToUpload { get; set; }
    }
}
