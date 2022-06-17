namespace TokanPages.Services.HttpClientService.Models;

using System.Net;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class ExecutionResult : HttpContentResult
{
    public HttpStatusCode StatusCode { get; set; }
}