using TokanPages.Backend.Utility.Abstractions;

namespace TokanPages.Services.HttpClientService.Abstractions;

public interface IHttpClientServiceFactory
{
    IHttpClientService Create(bool allowAutoRedirect, ILoggerService loggerService);
}