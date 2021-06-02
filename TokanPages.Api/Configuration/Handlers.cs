using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Polly.Extensions.Http;
using Polly;

namespace TokanPages.Api.Configuration
{
    public static class Handlers
    {
        public static IAsyncPolicy<HttpResponseMessage> RetryPolicyHandler()
        {
            const int RETRY_COUNT = 3;
            const double DURATION_BETWEEN_RETRIES = 150;
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .Or<TaskCanceledException>()
                .OrResult(AResponse => AResponse.StatusCode 
                    is HttpStatusCode.RequestTimeout 
                    or HttpStatusCode.BadGateway 
                    or HttpStatusCode.GatewayTimeout 
                    or HttpStatusCode.ServiceUnavailable
                ).WaitAndRetryAsync(RETRY_COUNT, ARetryCount 
                    => TimeSpan.FromMilliseconds(DURATION_BETWEEN_RETRIES * Math.Pow(2, ARetryCount - 1)));
        }
    }
}