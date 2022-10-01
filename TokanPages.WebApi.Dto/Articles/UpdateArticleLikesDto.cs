﻿using System.Diagnostics.CodeAnalysis;

namespace TokanPages.WebApi.Dto.Articles;

/// <summary>
/// Use it when you want to update likes
/// </summary>
[ExcludeFromCodeCoverage]
public class UpdateArticleLikesDto
{
    /// <summary>
    /// Mandatory
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Mandatory
    /// </summary>
    public int AddToLikes { get; set; }
}