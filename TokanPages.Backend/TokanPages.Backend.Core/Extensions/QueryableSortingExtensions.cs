using System.Diagnostics.CodeAnalysis;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using TokanPages.Backend.Core.Paging;

namespace TokanPages.Backend.Core.Extensions;

[ExcludeFromCodeCoverage]
public static class QueryableSortingExtensions
{
    public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> databaseQuery, PagingInfo pagingInfo, IDictionary<string, Expression<Func<T, object>>> orderByExpressions)
    {
        if (string.IsNullOrEmpty(pagingInfo.OrderByColumn) || pagingInfo.OrderByAscending == null)
            return databaseQuery;

        if (orderByExpressions.ContainsKey(pagingInfo.OrderByColumn))
        {
            return pagingInfo.OrderByAscending ?? true
                ? databaseQuery.OrderBy(orderByExpressions[pagingInfo.OrderByColumn])
                : databaseQuery.OrderByDescending(orderByExpressions[pagingInfo.OrderByColumn]);
        }

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