namespace TokanPages.Backend.Core.Models.Paging;

using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class PagingInfo
{
    public int PageNumber { get; set; }

    public int PageSize { get; set; }

    public string? OrderByColumn { get; set; }

    public bool? OrderByAscending { get; set; }
}