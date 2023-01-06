using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.MigrationRunner.Helpers;

[ExcludeFromCodeCoverage]
public static class InputArguments
{
    public static IList<string> Normalize(IReadOnlyCollection<string> arguments)
    {
        if (arguments.Count <= 0) 
            return new List<string>();

        var result = new List<string>();
        foreach (var argument in arguments)
        {
            var item = argument.ToLower();
            result.Add(item);
        }

        return result;
    }
}