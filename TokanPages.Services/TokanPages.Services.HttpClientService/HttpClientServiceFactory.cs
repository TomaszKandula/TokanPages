using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Services.HttpClientService.Abstractions;

namespace TokanPages.Services.HttpClientService;

internal sealed class HttpClientServiceFactory : IHttpClientServiceFactory
{
    public IHttpClientService Create(bool allowAutoRedirect, ILoggerService loggerService)
    {
        var handler = new HttpClientHandler { AllowAutoRedirect = allowAutoRedirect };
        var client = new HttpClient(handler);
        return new HttpClientService(client, loggerService);
    }
}