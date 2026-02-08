using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using TokanPages.Services.VideoConverterService.Abstractions;

namespace TokanPages.Services.VideoConverterService;

[ExcludeFromCodeCoverage]
public static class AssemblyConfigurer
{
    public static void AddVideoConverter(this IServiceCollection services)
    {
        services.AddSingleton<IVideoConverter, VideoConverter>();
    }
}