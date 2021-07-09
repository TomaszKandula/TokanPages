using System;
using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Shared.Dto.Articles
{
    [ExcludeFromCodeCoverage]
    public class UpdateArticleVisibilityDto
    {
        public Guid Id { get; set; }
        
        public bool IsPublished { get; set; }
    }
}
