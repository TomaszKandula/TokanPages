namespace TokanPages.WebApi.Dto.Articles;

using System;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Use it when you want to update count
/// </summary>
[ExcludeFromCodeCoverage]
public class UpdateArticleCountDto
{
    /// <summary>
    /// Mandatory
    /// </summary>
    public Guid Id { get; set; }
}