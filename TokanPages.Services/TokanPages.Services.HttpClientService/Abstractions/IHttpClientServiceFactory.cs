using TokanPages.Backend.Core.Utilities.LoggerService;

namespace TokanPages.Services.HttpClientService.Abstractions;

public interface IHttpClientServiceFactory
{
    IHttpClientService Create(bool allowAutoRedirect, ILoggerService loggerService);
}