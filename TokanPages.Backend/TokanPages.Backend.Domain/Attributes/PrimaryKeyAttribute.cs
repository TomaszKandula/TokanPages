using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Domain.Attributes;

[ExcludeFromCodeCoverage]
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited =  true)]
public class PrimaryKeyAttribute : Attribute { }