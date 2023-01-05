using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Shared.Services;

[ExcludeFromCodeCoverage]
public static class ConsolePrints
{
    public static void PrintOnInfo(string text)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(text);
    }

    public static void PrintOnSuccess(string text)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(text);
    }

    public static void PrintOnWarning(string text)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(text);
    }

    public static void PrintOnError(string text)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(text);
    }
}