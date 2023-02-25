using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Shared.Services;

[SuppressMessage("Sonar Code Smell", "S3267:Loop should be simplified with LINQ expression", 
    Justification = "LINQ would actually just make things harder to understand")]
public static class PasswordHelpers
{
    public static bool HaveSpecialCharacter(string value)
    {
        var characters = new [] { '!', '@', '#', '$', '%', '^', '&', '*' };
        foreach (var character in value)
        {
            if (characters.Contains(character)) 
                return true;
        }

        return false;
    }

    public static bool ContainNumber(string value)
    {
        foreach (var character in value)
        {
            if (character >= 48 && character <= 57)
                return true;
        }

        return false;
    }

    public static bool HaveLargeLetter(string value)
    {
        foreach (var character in value)
        {
            if (character >= 65 && character <= 90)
                return true;
        }

        return false;
    }

    public static bool HaveSmallLetter(string value)
    {
        foreach (var character in value)
        {
            if (character >= 97 && character <= 122)
                return true;
        }

        return false;
    }
}