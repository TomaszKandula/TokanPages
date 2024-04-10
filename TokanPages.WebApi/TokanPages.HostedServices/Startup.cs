using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Serilog;
using TokanPages.HostedServices.Services.Abstractions;
using TokanPages.HostedServices.Services.CronJobs;
using TokanPages.HostedServices.Services.Models;

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
    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <summary>
    /// Application services.
    /// </summary>
    /// <param name="services">IServiceCollection.</param>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers().AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.Converters.Add(new StringEnumConverter());
            options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        });

        services.AddResponseCaching();
        services.AddApiVersioning(options =>
        {
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ErrorResponses = new ApiVersionException();
        });

        services.AddResponseCompression(options => options.Providers.Add<GzipCompressionProvider>());
        services.RegisterDependencies(_configuration);
        services.SetupDockerInternalNetwork();
        
         var batchProcessingConfig = new BatchProcessingConfig
         {
             TimeZoneInfo = TimeZoneInfo.Local,
             CronExpression = @"*/15 * * * *"
         };

        services.AddSingleton<IBatchProcessingConfig>(batchProcessingConfig);
        services.AddHostedService<BatchProcessingJob>();
    }

    /// <summary>
    /// Application configuration.
    /// </summary>
    /// <param name="builder">IApplicationBuilder.</param>
    public void Configure(IApplicationBuilder builder)
    {
        builder.UseSerilogRequestLogging();
        builder.UseForwardedHeaders();
        builder.UseHttpsRedirection();
        builder.UseResponseCaching();
        builder.UseResponseCompression();
        builder.UseRouting();
        builder.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGet("/", context 
                => context.Response.WriteAsync("Hosted Services API"));
            endpoints.MapGet("/hc/ready", context 
                => context.Response.WriteAsync("{\"status\": \"live\"}"));
        });
    }
}