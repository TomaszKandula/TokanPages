namespace TokanPages.Gateway.Services.Abstractions;

/// <summary>
/// 
/// </summary>
public interface IProxyHttpClient
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="httpRequestMessage"></param>
    /// <param name="context"></param>
    /// <param name="responseHeadersExclude"></param>
    /// <returns></returns>
    Task ProxyRequest(HttpRequestMessage httpRequestMessage, HttpContext context, IEnumerable<string> responseHeadersExclude);
}