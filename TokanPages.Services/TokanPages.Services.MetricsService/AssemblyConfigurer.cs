using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using TokanPages.Services.MetricsService.Abstractions;

namespace TokanPages.Services.MetricsService;

[ExcludeFromCodeCoverage]
public static class AssemblyConfigurer
{
    public static void AddMetricsService(this IServiceCollection services)
    {
        services.AddScoped<IMetricsService, MetricsService>();
    }
}