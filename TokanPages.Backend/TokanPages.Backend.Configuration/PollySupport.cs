using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TokanPages.Backend.Shared.Constants;

namespace TokanPages.Backend.Configuration;

public static class PollySupport
{
    public static void SetupRetryPolicyWithPolly(IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
    {
        var developmentOrigin = configuration.GetValue<string>("Paths_DevelopmentOrigin");
        var deploymentOrigin = configuration.GetValue<string>("Paths_DeploymentOrigin");

        services.AddHttpClient("RetryHttpClient", options =>
        {
            options.BaseAddress = new Uri(environment.IsDevelopment() 
                ? developmentOrigin 
                : deploymentOrigin);
            options.DefaultRequestHeaders.Add("Accept", ContentTypes.Json);
            options.Timeout = TimeSpan.FromMinutes(5);
            options.DefaultRequestHeaders.ConnectionClose = true;
        }).AddPolicyHandler(HttpPolicyHandlers.SetupRetry());
    }
}