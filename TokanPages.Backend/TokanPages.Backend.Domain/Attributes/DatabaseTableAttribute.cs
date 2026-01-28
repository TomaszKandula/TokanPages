using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Domain.Attributes;

[ExcludeFromCodeCoverage]
[AttributeUsage(AttributeTargets.Class)]
public class DatabaseTableAttribute : Attribute
{
    public string TableName { get; set; } = "";
    public string Schema { get; set; } = "dbo";
}
