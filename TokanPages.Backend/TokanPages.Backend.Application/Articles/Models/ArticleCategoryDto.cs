namespace TokanPages.Backend.Application.Articles.Models;

/// <summary>
/// Gets list of article category IDs.
/// </summary>
public class ArticleCategoryDto
{
    public Guid Id { get; set; }

    public string CategoryName { get; set; } = "";
}