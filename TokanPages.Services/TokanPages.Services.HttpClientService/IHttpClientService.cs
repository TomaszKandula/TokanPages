using TokanPages.Services.HttpClientService.Models;

namespace TokanPages.Services.HttpClientService;

public interface IHttpClientService
{
    Task<ExecutionResult> Execute(Configuration configuration, CancellationToken cancellationToken = default);

    Task<T> Execute<T>(Configuration configuration, CancellationToken cancellationToken = default);
}