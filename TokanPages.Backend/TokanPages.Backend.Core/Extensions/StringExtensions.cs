namespace TokanPages.Backend.Core.Extensions
{
    using System;
    using System.Text;
    using System.Globalization;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public static class StringExtensions
    {
        public static bool IsGuid(this string AValue) 
            => Guid.TryParse(AValue.Replace("\"", ""), out var _);

        public static string ToBase64Encode(this string APlainText)
            => Convert.ToBase64String(Encoding.UTF8.GetBytes(APlainText));

        public static string CapitalizeEachWord(this string AInput)
            => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(AInput.ToLower());

        public static string ToBase64Decode(this string ABase64EncodedData)
            => Encoding.UTF8.GetString(Convert.FromBase64String(ABase64EncodedData));
		
        public static bool IsBase64String(this string ABase64)
        {
            var LBuffer = new Span<byte>(new byte[ABase64.Length]);
            var LWidth = ABase64.Length / 4 * 4 + (ABase64.Length % 4 == 0 ? 0 : 4);
            return Convert.TryFromBase64String(ABase64.PadRight(LWidth, '='), LBuffer, out _);
        }
    }
}