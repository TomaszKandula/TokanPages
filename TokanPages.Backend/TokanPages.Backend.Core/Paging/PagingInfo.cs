using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Core.Models.Paging;

[ExcludeFromCodeCoverage]
public class PagingInfo
{
    public int PageNumber { get; set; }

    public int PageSize { get; set; }

    public string? OrderByColumn { get; set; }

    public bool? OrderByAscending { get; set; }
}