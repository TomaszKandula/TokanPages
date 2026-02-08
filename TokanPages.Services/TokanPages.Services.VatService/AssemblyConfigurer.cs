using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using TokanPages.Services.VatService.Abstractions;

namespace TokanPages.Services.VatService;

[ExcludeFromCodeCoverage]
public static class AssemblyConfigurer
{
    public static void AddVatService(this IServiceCollection services)
    {
        services.AddScoped<IVatService, VatService>();
    }
}