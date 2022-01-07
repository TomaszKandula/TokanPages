namespace TokanPages.Backend.Core.Extensions;

using System;
using System.Text;
using System.Linq;
using System.Globalization;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public static class StringExtensions
{
    public static bool IsGuid(this string value) 
        => Guid.TryParse(value.Replace("\"", ""), out var _);

    public static string ToBase64Encode(this string plainText)
        => Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText));

    public static string CapitalizeEachWord(this string input)
        => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input.ToLower());

    public static string ToBase64Decode(this string base64EncodedData)
        => Encoding.UTF8.GetString(Convert.FromBase64String(base64EncodedData));
		
    public static bool IsBase64String(this string base64)
    {
        var buffer = new Span<byte>(new byte[base64.Length]);
        var width = base64.Length / 4 * 4 + (base64.Length % 4 == 0 ? 0 : 4);
        return Convert.TryFromBase64String(base64.PadRight(width, '='), buffer, out _);
    }

    public static string MakeBody(this string template, IDictionary<string, string> items)
    {
        if (string.IsNullOrEmpty(template) || string.IsNullOrWhiteSpace(template))
            return null;
            
        if (items == null) 
            return null;
            
        var templateItemModels = items.ToList();
        if (!templateItemModels.Any()) 
            return null;

        return templateItemModels.Aggregate(template, (current, item) 
            => current.Replace(item.Key, item.Value));
    }
}