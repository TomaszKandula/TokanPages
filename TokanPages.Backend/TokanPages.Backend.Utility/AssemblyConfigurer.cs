using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using TokanPages.Backend.UtilityService.Abstractions;

namespace TokanPages.Backend.UtilityService;

[ExcludeFromCodeCoverage]
public static class AssemblyConfigurer
{
    public static void AddUtilityServices(this IServiceCollection services)
    {
        services.AddSingleton<ILoggerService, LoggerService>();
        services.AddScoped<IJsonSerializer, JsonSerializer>();
        services.AddScoped<IDateTimeService, DateTimeService>();
        services.AddScoped<IDataUtilityService, DataUtilityService>();
    }
}