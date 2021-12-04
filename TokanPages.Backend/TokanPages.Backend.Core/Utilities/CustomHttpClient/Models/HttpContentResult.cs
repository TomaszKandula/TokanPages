#nullable enable

namespace TokanPages.Backend.Core.Utilities.CustomHttpClient.Models
{
    using System.Net.Http.Headers;

    public class HttpContentResult
    {
        public MediaTypeHeaderValue? ContentType { get; set; }

        public byte[]? Content { get; set; }
    }
}