using System;
using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Shared.Dto.Articles
{
    [ExcludeFromCodeCoverage]
    public class UpdateArticleContentDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public string TextToUpload { get; set; }
        
        public string ImageToUpload { get; set; }
    }
}
