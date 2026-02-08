using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using TokanPages.Services.HttpClientService.Abstractions;

namespace TokanPages.Services.HttpClientService;

[ExcludeFromCodeCoverage]
public static class AssemblyConfigurer
{
    public static void AddHttpClientService(this IServiceCollection services)
    {
        services.AddSingleton<IHttpClientServiceFactory>(_ => new HttpClientServiceFactory());
    }
}