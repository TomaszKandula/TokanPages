using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using TokanPages.Services.CipheringService.Abstractions;

namespace TokanPages.Services.CipheringService;

[ExcludeFromCodeCoverage]
public static class AssemblyConfigurer
{
    public static void AddCipheringService(this IServiceCollection services)
    {
        services.AddScoped<ICipheringService, CipheringService>();
    }
}