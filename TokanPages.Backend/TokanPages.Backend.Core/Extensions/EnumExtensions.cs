using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace TokanPages.Backend.Core.Extensions;

[ExcludeFromCodeCoverage]
public static class EnumExtensions
{
    public static string GetDescription(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString());

        if (field?.GetCustomAttributes(typeof(DescriptionAttribute), false) 
                is DescriptionAttribute[] attributes && attributes.Any())
            return attributes.First().Description;

        return value.ToString();
    }
}