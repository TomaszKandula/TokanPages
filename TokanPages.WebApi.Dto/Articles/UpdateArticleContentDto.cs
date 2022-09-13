﻿using System.Diagnostics.CodeAnalysis;

namespace TokanPages.WebApi.Dto.Articles;

/// <summary>
/// Use it when you want to update existing content
/// </summary>
[ExcludeFromCodeCoverage]
public class UpdateArticleContentDto
{
    /// <summary>
    /// Mandatory
    /// </summary>
    public Guid Id { get; set; }

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