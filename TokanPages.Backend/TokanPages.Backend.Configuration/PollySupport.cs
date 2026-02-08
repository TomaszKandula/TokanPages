using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TokanPages.Backend.Shared.Constants;
using TokanPages.Backend.Shared.Options;

namespace TokanPages.Backend.Configuration;

[ExcludeFromCodeCoverage]
internal static class PollySupport
{
    internal static void SetupRetryPolicyWithPolly(IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
    {
        var settings = configuration.GetAppSettings();
        var developmentOrigin = settings.PathsDevelopmentOrigin;
        var deploymentOrigin = settings.PathsDeploymentOrigin;
        var url = environment.IsDevelopment() ? developmentOrigin : deploymentOrigin;

        services.AddHttpClient("RetryHttpClient", options =>
        {
            options.BaseAddress = new Uri(url);
            options.DefaultRequestHeaders.Add("Accept", ContentTypes.Json);
            options.Timeout = TimeSpan.FromMinutes(5);
            options.DefaultRequestHeaders.ConnectionClose = true;
        }).AddPolicyHandler(HttpPolicyHandlers.SetupRetry());
    }
}