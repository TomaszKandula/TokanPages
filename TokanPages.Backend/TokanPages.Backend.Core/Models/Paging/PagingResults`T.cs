namespace TokanPages.Backend.Core.Models.Paging;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class PagingResults<T> where T : class
{
    public PagingInfo? PagingInfo { get; set; }

    public int TotalSize { get; set; }

    public ICollection<T>? Results { get; set; }
}