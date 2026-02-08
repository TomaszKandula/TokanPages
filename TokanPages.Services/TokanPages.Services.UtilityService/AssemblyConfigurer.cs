using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using TokanPages.Services.UtilityService.Abstractions;

namespace TokanPages.Services.UtilityService;

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