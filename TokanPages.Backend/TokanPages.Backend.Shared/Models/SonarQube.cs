using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Shared.Models
{
    [ExcludeFromCodeCoverage]
    public class SonarQube
    {
        public string Server { get; set; }
        public string Token { get; set; }
    }
}