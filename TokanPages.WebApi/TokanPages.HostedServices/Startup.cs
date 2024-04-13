using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Configuration;
using Serilog;

namespace TokanPages.HostedServices;

/// <summary>
/// Startup.
/// </summary>
[ExcludeFromCodeCoverage]
public class Startup
{
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Startup implementation.
    /// </summary>
    /// <param name="configuration">Application configuration.</param>
    public Startup(IConfiguration configuration) => _configuration = configuration;

    /// <summary>
    /// Application services.
    /// </summary>
    /// <param name="services">IServiceCollection.</param>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.RegisterDependencies(_configuration);
        services.SetupDockerInternalNetwork();
    }

    /// <summary>
    /// Application configuration.
    /// </summary>
    /// <param name="builder">IApplicationBuilder.</param>
    public void Configure(IApplicationBuilder builder)
    {
        builder.UseSerilogRequestLogging();
    }
}