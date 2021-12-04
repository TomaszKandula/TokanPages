#nullable enable

namespace TokanPages.Backend.Core.Utilities.CustomHttpClient.Models
{
    using System.Net;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class Results : HttpContentResult
    {
        public HttpStatusCode StatusCode { get; set; }
    }
}