namespace TokanPages.Backend.Shared.Models
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class SonarQube
    {
        public string Server { get; set; }
        public string Token { get; set; }
    }
}