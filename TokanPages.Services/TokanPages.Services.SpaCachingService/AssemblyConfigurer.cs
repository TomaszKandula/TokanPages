using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using TokanPages.Services.SpaCachingService.Abstractions;

namespace TokanPages.Services.SpaCachingService;

[ExcludeFromCodeCoverage]
public static class AssemblyConfigurer
{
    public static void AddCachingService(this IServiceCollection services)
    {
        services.AddSingleton<ICachingService, CachingService>();
    }
}