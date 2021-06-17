using System;
using System.Linq;
using System.Linq.Expressions;
using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Core.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class QueryableExtensions
    {
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> ASet, bool ACondition, 
            Expression<Func<T, bool>> AFunc)
            => ACondition ? ASet.Where(AFunc) : ASet;

        public static IQueryable<T> WhereIfElse<T>(this IQueryable<T> ASet, bool ACondition, 
            Expression<Func<T, bool>> AIfFunc, Expression<Func<T, bool>> AElseFunc)
            => ASet.Where(ACondition ? AIfFunc : AElseFunc);

        public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> AExpression)
        {
            var LCandidateExpr = AExpression.Parameters[0];
            var LBody = Expression.Not(AExpression.Body);
            return Expression.Lambda<Func<T, bool>>(LBody, LCandidateExpr);
        }
    }
}
