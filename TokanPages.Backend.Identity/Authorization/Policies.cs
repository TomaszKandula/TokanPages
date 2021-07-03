using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Identity.Authorization
{
    [ExcludeFromCodeCoverage]
    public static class Policies
    {
        public static string AccessToTokanPages => nameof(AccessToTokanPages);
    }
}