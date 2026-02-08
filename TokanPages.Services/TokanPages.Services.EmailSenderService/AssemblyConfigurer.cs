using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using TokanPages.Services.EmailSenderService.Abstractions;

namespace TokanPages.Services.EmailSenderService;

[ExcludeFromCodeCoverage]
public static class AssemblyConfigurer
{
    public static void AddEmailSenderService(this IServiceCollection services)
    {
        services.AddScoped<IEmailSenderService, EmailSenderService>();
    }
}