﻿using System.Diagnostics.CodeAnalysis;

namespace TokanPages.WebApi.Dto.Articles;

/// <summary>
/// Use it when you want to add article
/// </summary>
[ExcludeFromCodeCoverage]
public class AddArticleDto
{
    /// <summary>
    /// Mandatory
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Mandatory
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Mandatory
    /// </summary>
    public string? TextToUpload { get; set; }

    /// <summary>
    /// Mandatory
    /// </summary>
    public string? ImageToUpload { get; set; }
}