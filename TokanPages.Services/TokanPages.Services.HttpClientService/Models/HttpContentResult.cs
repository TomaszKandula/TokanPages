#nullable enable
namespace TokanPages.Services.HttpClientService.Models;

using System.Net.Http.Headers;

public class HttpContentResult
{
    public MediaTypeHeaderValue? ContentType { get; set; }

    public byte[]? Content { get; set; }
}