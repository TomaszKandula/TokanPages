using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using TokanPages.Services.PayUService.Abstractions;

namespace TokanPages.Services.PayUService;

[ExcludeFromCodeCoverage]
public static class AssemblyConfigurer
{
    public static void AddPayUService(this IServiceCollection services)
    {
        services.AddScoped<IPayUService, PayUService>();
    }
}