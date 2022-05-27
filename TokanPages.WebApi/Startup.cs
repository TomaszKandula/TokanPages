namespace TokanPages.WebApi;

using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.ResponseCompression;
using Serilog;
using Middleware;
using Configuration;
using Backend.Core.Exceptions;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

/// <summary>
/// Startup
/// </summary>
[ExcludeFromCodeCoverage]
public class Startup
{
    private readonly IConfiguration _configuration;

    private readonly IHostEnvironment _environment;

    /// <summary>
    /// Startup
    /// </summary>
    /// <param name="configuration">Provided configuration</param>
    /// <param name="environment">Application host environment</param>
    public Startup(IConfiguration configuration, IHostEnvironment environment)
    {
        _configuration = configuration;
        _environment = environment;
    }

    /// <summary>
    /// Services
    /// </summary>
    /// <param name="services">Service collection</param>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCors();
        services.AddControllers().AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.Converters.Add(new StringEnumConverter());
            options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        });
        services.AddApiVersioning(options =>
        {
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ErrorResponses = new ApiVersionException();
        });
        services.AddResponseCompression(options => options.Providers.Add<GzipCompressionProvider>());
        services.RegisterDependencies(_configuration, _environment);
        services.SetupRedisCache(_configuration);
        services.SetupSwaggerOptions(_environment);
        services.SetupDockerInternalNetwork();
    }

    /// <summary>
    /// Configure
    /// </summary>
    /// <param name="builder">Application builder</param>
    public void Configure(IApplicationBuilder builder)
    {
        builder.UseSerilogRequestLogging();

        builder.UseForwardedHeaders();
        builder.UseHttpsRedirection();
        builder.ApplyCorsPolicy(_configuration);

        builder.UseMiddleware<Exceptions>();
        builder.UseMiddleware<CacheControl>();

        builder.UseResponseCompression();
        builder.UseRouting();

        builder.UseAuthentication();
        builder.UseAuthorization();
        builder.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGet("/", context => context.Response.WriteAsync("Tokan Pages API"));
        });

        builder.SetupSwaggerUi(_configuration, _environment);
    }
}