#nullable enable

namespace TokanPages.Backend.Core.Utilities.CustomHttpClient.Models
{
    using System.Net;
    using System.Net.Http.Headers;

    public class Results
    {
        public HttpStatusCode StatusCode { get; set; }

        public MediaTypeHeaderValue? ContentType { get; set; }
        
        public byte[]? Content { get; set; }
    }
}