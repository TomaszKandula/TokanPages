namespace TokanPages.Backend.Shared.Dto.Articles
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class UpdateArticleVisibilityDto
    {
        public Guid Id { get; set; }
        
        public bool IsPublished { get; set; }
    }
}