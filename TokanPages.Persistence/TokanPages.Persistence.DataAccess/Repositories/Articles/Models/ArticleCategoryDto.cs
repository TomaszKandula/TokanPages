using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.Articles.Models;

/// <summary>
/// Gets list of article category IDs.
/// </summary>
[ExcludeFromCodeCoverage]
public class ArticleCategoryDto
{
    public Guid Id { get; set; }

    public string CategoryName { get; set; } = "";
}