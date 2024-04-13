using TokanPages.Gateway.Services.Abstractions;

namespace TokanPages.Gateway.Services;

/// <summary>
/// Proxy HTTP client implementation.
/// </summary>
public class ProxyHttpClient : IProxyHttpClient
{
    private readonly HttpClient _httpClient;

    /// <summary>
    /// Base constructor.
    /// </summary>
    public ProxyHttpClient(HttpClient httpClient) => _httpClient = httpClient;

    /// <inheritdoc />
    public async Task ProxyRequest(HttpRequestMessage httpRequestMessage, HttpContext context, IEnumerable<string> responseHeadersExclude)
    {
        var responseMessage = await _httpClient.SendAsync(httpRequestMessage, HttpCompletionOption.ResponseHeadersRead, context.RequestAborted);
        context.Response.StatusCode = (int)responseMessage.StatusCode;

        foreach (var valuePair in responseMessage.Headers
                     .Where(valuePair => !responseHeadersExclude.Contains(valuePair.Key.ToLower())))
        {
            context.Response.Headers[valuePair.Key] = valuePair.Value.ToArray();
        }

        foreach (var valuePair in responseMessage.Content.Headers)
        {
            context.Response.Headers[valuePair.Key] = valuePair.Value.ToArray();
        }

        context.Response.Headers.Remove("transfer-encoding");
        await responseMessage.Content.CopyToAsync(context.Response.Body);
    }
}