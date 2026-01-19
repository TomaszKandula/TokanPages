using System.Diagnostics.CodeAnalysis;
using TokanPages.Persistence.DataAccess.Repositories.Articles.Models;

namespace TokanPages.Backend.Application.Articles.Commands;

[ExcludeFromCodeCoverage]
public class RetrieveArticleInfoCommandResult
{
    public List<ArticleDataDto> Articles { get; set; } = new();
}
