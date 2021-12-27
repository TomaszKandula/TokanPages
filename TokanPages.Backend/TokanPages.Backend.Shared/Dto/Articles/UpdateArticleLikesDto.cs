namespace TokanPages.Backend.Shared.Dto.Articles;

using System;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class UpdateArticleLikesDto
{
    public Guid Id { get; set; }
        
    public int AddToLikes { get; set; }
}