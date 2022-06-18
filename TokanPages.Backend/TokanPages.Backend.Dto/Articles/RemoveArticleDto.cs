namespace TokanPages.Backend.Dto.Articles;

using System;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Use it when you want to remove existing article
/// </summary>
[ExcludeFromCodeCoverage]
public class RemoveArticleDto
{
    /// <summary>
    /// Mandatory
    /// </summary>
    public Guid Id { get; set; }
}