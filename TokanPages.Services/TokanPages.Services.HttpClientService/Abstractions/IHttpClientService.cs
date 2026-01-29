using Microsoft.AspNetCore.Http;
using TokanPages.Services.HttpClientService.Models;

namespace TokanPages.Services.HttpClientService.Abstractions;

public interface IHttpClientService
{
    Task ProxyRequest(HttpClientSettings configuration, HttpResponse response, CancellationToken cancellationToken = default);

    Task<ExecutionResult> Execute(HttpClientSettings configuration, CancellationToken cancellationToken = default);

    Task<T> Execute<T>(HttpClientSettings configuration, CancellationToken cancellationToken = default);
}