using Microsoft.AspNetCore.Http;
using TokanPages.Services.HttpClientService.Models;

namespace TokanPages.Services.HttpClientService.Abstractions;

public interface IHttpClientService
{
    Task ProxyRequest(Configuration configuration, HttpResponse response, CancellationToken cancellationToken = default);

    Task<ExecutionResult> Execute(Configuration configuration, CancellationToken cancellationToken = default);

    Task<T> Execute<T>(Configuration configuration, CancellationToken cancellationToken = default);
}