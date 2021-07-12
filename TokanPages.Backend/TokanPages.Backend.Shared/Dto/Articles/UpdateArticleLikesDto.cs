using System;
using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Shared.Dto.Articles
{
    [ExcludeFromCodeCoverage]
    public class UpdateArticleLikesDto
    {
        public Guid Id { get; set; }
        
        public int AddToLikes { get; set; }
    }
}
