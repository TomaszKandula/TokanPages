using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Core.Paging;

namespace TokanPages.Persistence.DataAccess.Repositories.Articles.Models;

[ExcludeFromCodeCoverage]
public class ArticleListRequest : PagingInfo
{
    public bool IsPublished { get; set; }

    public string? SearchTerm { get; set; }

    public Guid? CategoryId { get; set; }
}