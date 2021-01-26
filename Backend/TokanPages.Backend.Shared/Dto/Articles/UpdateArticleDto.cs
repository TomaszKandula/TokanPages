using System;

namespace TokanPages.Backend.Shared.Dto.Articles
{
    public class UpdateArticleDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string TextToUpload { get; set; }
        public string ImageToUpload { get; set; }
        public bool? IsPublished { get; set; }
        public int? Likes { get; set; }
        public int? ReadCount { get; set; }
    }
}
