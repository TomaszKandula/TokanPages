using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Application.Articles.Models;

namespace TokanPages.Backend.Application.Articles.Commands;

[ExcludeFromCodeCoverage]
public class RetrieveArticleInfoCommandResult
{
    public List<ArticleDataDto> Articles { get; set; } = new();
}
