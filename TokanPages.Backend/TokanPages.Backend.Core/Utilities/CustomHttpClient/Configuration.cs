#nullable enable

namespace TokanPages.Backend.Core.Utilities.CustomHttpClient
{
    using Authentication;

    public struct Configuration
    {
        public string Url { get; set; }

        public string Method { get; set; }

        public IAuthentication? Authentication { get; set; }            
    }
}