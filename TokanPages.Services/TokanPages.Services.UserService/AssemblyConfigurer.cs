using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Services.UserService;

[ExcludeFromCodeCoverage]
public static class AssemblyConfigurer
{
    public static void AddUserService(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
    }
}