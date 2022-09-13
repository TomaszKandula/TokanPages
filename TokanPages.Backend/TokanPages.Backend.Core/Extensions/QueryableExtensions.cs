using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;

namespace TokanPages.Backend.Core.Extensions;

[ExcludeFromCodeCoverage]
public static class QueryableExtensions
{
    public static IQueryable<T> WhereIf<T>(this IQueryable<T> set, bool condition, 
        Expression<Func<T, bool>> func)
        => condition ? set.Where(func) : set;

    public static IQueryable<T> WhereIfElse<T>(this IQueryable<T> set, bool condition, 
        Expression<Func<T, bool>> ifFunc, Expression<Func<T, bool>> elseFunc)
        => set.Where(condition ? ifFunc : elseFunc);

    public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> expression)
    {
        var candidateExpr = expression.Parameters[0];
        var body = Expression.Not(expression.Body);
        return Expression.Lambda<Func<T, bool>>(body, candidateExpr);
    }
}