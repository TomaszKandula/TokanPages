#nullable enable
namespace TokanPages.Services.HttpClientService.Models;

using System.Net;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class Results : HttpContentResult
{
    public HttpStatusCode StatusCode { get; set; }
}