namespace TokanPages.Backend.Core.Extensions;

using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

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