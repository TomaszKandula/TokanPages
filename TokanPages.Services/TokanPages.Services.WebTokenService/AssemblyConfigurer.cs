using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using TokanPages.Services.WebTokenService.Abstractions;

namespace TokanPages.Services.WebTokenService;

[ExcludeFromCodeCoverage]
public static class AssemblyConfigurer
{
    public static void AddWebTokenService(this IServiceCollection services)
    {
        services.AddScoped<IWebTokenUtility, WebTokenUtility>();
        services.AddScoped<IWebTokenValidation, WebTokenValidation>();
    }
}