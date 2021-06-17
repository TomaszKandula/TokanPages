using System;
using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Shared.Dto.Articles
{
    [ExcludeFromCodeCoverage]
    public class RemoveArticleDto
    {
        public Guid Id { get; set; }
    }
}
