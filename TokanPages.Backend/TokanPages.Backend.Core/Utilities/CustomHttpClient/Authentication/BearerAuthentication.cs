namespace TokanPages.Backend.Core.Utilities.CustomHttpClient.Authentication
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class BearerAuthentication : IAuthentication
    {
        public string Token { get; set; }
    }
}