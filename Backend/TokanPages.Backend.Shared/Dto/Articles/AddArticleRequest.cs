﻿using System;

namespace TokanPages.Backend.Shared.Dto.Articles
{
    public class AddArticleRequest
    {
        public string Title { get; set; }
        public string Desc { get; set; }
        public string Text { get; set; }
    }
}
