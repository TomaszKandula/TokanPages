using System.Diagnostics.CodeAnalysis;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using TokanPages.Backend.Core.Paging;

namespace TokanPages.Backend.Core.Extensions;

[ExcludeFromCodeCoverage]
public static class QueryableSortingExtensions
{
    public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> databaseQuery, PagingInfo pagingInfo, IDictionary<string, Expression<Func<T, object>>?> orderByExpressions)
    {
        if (string.IsNullOrEmpty(pagingInfo.OrderByColumn) || pagingInfo.OrderByAscending == null || !orderByExpressions.TryGetValue(pagingInfo.OrderByColumn, out var value))
            return databaseQuery;

        if (value != null)
            return pagingInfo.OrderByAscending ?? true
                ? databaseQuery.OrderBy(value)
                : databaseQuery.OrderByDescending(value);

        return databaseQuery;
    }

    public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> queryable, PagingInfo pagingInfo)
    {
        if (string.IsNullOrEmpty(pagingInfo.OrderByColumn) && pagingInfo.OrderByAscending != null) 
            return queryable;
            
        var sortOrder = pagingInfo.OrderByAscending ?? true ? "asc" : "desc";
        queryable = queryable.OrderBy($"{pagingInfo.OrderByColumn} {sortOrder}");

        return queryable;
    }

    public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> queryable, PagingInfo pagingInfo)
    {
        var skipCount = (pagingInfo.PageNumber - 1) * pagingInfo.PageSize;
        queryable = queryable
            .Skip(skipCount)
            .Take(pagingInfo.PageSize);

        return queryable;
    }
}