using System.Diagnostics.CodeAnalysis;
using System.Net;
using Polly;
using Polly.Extensions.Http;

namespace TokanPages.Backend.Configuration;

/// <summary>
/// HTTP Policy.
/// </summary>
[ExcludeFromCodeCoverage]
public static class HttpPolicyHandlers
{
    /// <summary>
    /// Setup retry policy.
    /// </summary>
    /// <returns>HttpResponseMessage.</returns>
    public static IAsyncPolicy<HttpResponseMessage> SetupRetry()
    {
        const int retryCount = 3;
        const double durationBetweenRetries = 150;

        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .Or<TaskCanceledException>()
            .OrResult(response => response.StatusCode 
                is HttpStatusCode.RequestTimeout 
                or HttpStatusCode.BadGateway 
                or HttpStatusCode.GatewayTimeout 
                or HttpStatusCode.ServiceUnavailable
            ).WaitAndRetryAsync(retryCount, count 
                => TimeSpan.FromMilliseconds(durationBetweenRetries * Math.Pow(2, count - 1)));
    }
}