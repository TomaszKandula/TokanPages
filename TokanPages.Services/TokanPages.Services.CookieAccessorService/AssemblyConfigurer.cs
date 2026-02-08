using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using TokanPages.Services.CookieAccessorService.Abstractions;

namespace TokanPages.Services.CookieAccessorService;

[ExcludeFromCodeCoverage]
public static class AssemblyConfigurer
{
    public static void AddCookieAccessor(this IServiceCollection services)
    {
        services.AddScoped<ICookieAccessor, CookieAccessor>();
    }
}