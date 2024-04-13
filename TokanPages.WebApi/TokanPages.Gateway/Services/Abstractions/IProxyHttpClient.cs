namespace TokanPages.Gateway.Services.Abstractions;

/// <summary>
/// Proxy HTTP client contract.
/// </summary>
public interface IProxyHttpClient
{
    /// <summary>
    /// Proxy given request.
    /// </summary>
    /// <param name="httpRequestMessage">HTTP request message.</param>
    /// <param name="context">HTTP context.</param>
    /// <param name="responseHeadersExclude">Excluded response headers.</param>
    /// <returns></returns>
    Task ProxyRequest(HttpRequestMessage httpRequestMessage, HttpContext context, IEnumerable<string> responseHeadersExclude);
}