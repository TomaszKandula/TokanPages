using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Shared.Models
{
    [ExcludeFromCodeCoverage]
    public class ParameterModel
    {
        public string Key { get; init; }
        public string Value { get; init; }
    }
}