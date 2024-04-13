using TokanPages.Gateway.Models;
using Microsoft.Extensions.Primitives;

namespace TokanPages.Gateway.Services;

/// <summary>
/// Request message factory implementation.
/// </summary>
public static class RequestMessageFactory
{
    private const string XForwardedHostHeaderName = "X-Forwarded-Host";

    private const string HostHeaderName = "host";

    /// <summary>
    /// Creates HTTP request message.
    /// </summary>
    /// <param name="request">HTTP request.</param>
    /// <param name="route">Given route.</param>
    /// <param name="messageHeaders">Request message.</param>
    /// <returns>Prepared HTTP request message.</returns>
    public static HttpRequestMessage Create(HttpRequest request, RouteDefinition route, RequestMessageHeaders? messageHeaders)
    {
        var requestMessage = new HttpRequestMessage();
        var requestMethod = request.Method;
        if (!HttpMethods.IsGet(requestMethod) &&
            !HttpMethods.IsHead(requestMethod) &&
            !HttpMethods.IsTrace(requestMethod))
        {
            var streamContent = new StreamContent(request.Body);
            requestMessage.Content = streamContent;
        }

        var headersToExclude = messageHeaders?.Exclude ?? Array.Empty<string>();
        var headers = request.Headers.Where(valuePair => !headersToExclude.Contains(valuePair.Key.ToLower()));
        foreach (var valuePair in headers)
        {
            TryAddHeaderWithoutValidation(requestMessage, valuePair);
        }

        if (!request.Headers.ContainsKey(XForwardedHostHeaderName) && request.Headers.TryGetValue(HostHeaderName, out var hostValue))
        {
            TryAddHeaderWithoutValidation(requestMessage, CreateHeaderKeyValuePair(XForwardedHostHeaderName, hostValue));
        }

        var uri = $"{route.Schema}://{route.Host}:{route.Port}{request.PathBase}{route.Path}{request.QueryString}";
        requestMessage.RequestUri = new Uri(uri);
        requestMessage.Method = new HttpMethod(request.Method);

        return requestMessage;
    }

    private static KeyValuePair<string, StringValues> CreateHeaderKeyValuePair(string headerKey, StringValues headerValue)
    {
        return new KeyValuePair<string, StringValues>(headerKey, headerValue);
    }

    private static void TryAddHeaderWithoutValidation(HttpRequestMessage requestMessage, KeyValuePair<string, StringValues> header)
    {
        _ = requestMessage.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray()) 
            || (requestMessage.Content?.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray())).GetValueOrDefault(false);
    }
}