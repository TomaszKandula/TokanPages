using Microsoft.Extensions.DependencyInjection;
using TokanPages.Services.BatchService.Abstractions;

namespace TokanPages.Services.BatchService;

public static class AssemblyConfigurer
{
    public static void AddBatchService(this IServiceCollection services)
    {
        services.AddScoped<IBatchService, BatchService>();
    }
}