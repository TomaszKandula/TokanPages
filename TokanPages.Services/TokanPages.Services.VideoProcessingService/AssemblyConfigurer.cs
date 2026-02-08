using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using TokanPages.Services.VideoProcessingService.Abstractions;

namespace TokanPages.Services.VideoProcessingService;

[ExcludeFromCodeCoverage]
public static class AssemblyConfigurer
{
    public static void AddVideoProcessor(this IServiceCollection services)
    {
        services.AddSingleton<IVideoProcessor, VideoProcessor>();
    }
}