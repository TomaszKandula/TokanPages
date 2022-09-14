using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Core.Extensions;

[ExcludeFromCodeCoverage]
public static class EnumerableExtensions
{
    public static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> source, bool condition, 
        Func<TSource, bool> predicate)
        => condition ? source.Where(predicate) : source;

    public static IEnumerable<TSource> WhereIfElse<TSource>(this IEnumerable<TSource> set, bool condition, 
        Func<TSource, bool> ifFunc, Func<TSource, bool> elseFunc)
        => set.Where(condition ? ifFunc : elseFunc);
}