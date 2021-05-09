using System.Linq;

namespace TokanPages.Backend.Core.Generators
{
    public abstract class StringProvider : BaseClass
    {
        public static string GetRandomEmail(int ALength = 12, string ADomain = "gmail.com") 
            => $"{GetRandomString(ALength)}@{ADomain}";

        public static string GetRandomString(int ALength = 12, string APrefix = "")
        {
            if (ALength == 0) 
                return string.Empty; 

            const string CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"; 

            var LString = new string(Enumerable.Repeat(CHARS, ALength)
                .Select(AString => AString[Random.Next(AString.Length)])
                .ToArray()); 

            if (!string.IsNullOrEmpty(APrefix) || !string.IsNullOrWhiteSpace(APrefix)) 
                return APrefix + LString; 

            return LString;
        }
    }
}