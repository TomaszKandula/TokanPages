#nullable enable

namespace TokanPages.Backend.Core.Utilities.CustomHttpClient.Models
{
    using System.Net.Http;
    using Authentication;

    public class Configuration
    {
        public string Url { get; set; }

        public string Method { get; set; }

        public IAuthentication? Authentication { get; set; }

        public StringContent? StringContent { get; set; } 
    }
}