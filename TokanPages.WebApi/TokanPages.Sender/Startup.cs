using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Serilog;
using TokanPages.Backend.Configuration;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Exceptions.Middleware;

namespace TokanPages.Sender;

/// <summary>
/// Startup.
/// </summary>
[ExcludeFromCodeCoverage]
public class Startup
{
    private const string ApiName = "Sender API";

    private const string DocVersion = "v1";

    private static readonly string[] XmlDocs = { "TokanPages.Sender.xml", "TokanPages.Sender.Dto.xml" };

    private readonly IConfiguration _configuration;

    private readonly IHostEnvironment _environment;

    /// <summary>
    /// Startup.
    /// </summary>
    /// <param name="configuration">Provided configuration.</param>
    /// <param name="environment">Application host environment.</param>
    public Startup(IConfiguration configuration, IHostEnvironment environment)
    {
        _configuration = configuration;
        _environment = environment;
    }

    /// <summary>
    /// Services.
    /// </summary>
    /// <param name="services">Service collection.</param>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCors();
        services.AddControllers().AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.Converters.Add(new StringEnumConverter());
            options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
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
        services.RegisterDependencies(_configuration, _environment);
        services.SetupRedisCache(_configuration);
        services.SetupSwaggerOptions(_environment, ApiName, DocVersion, XmlDocs);
        services.SetupDockerInternalNetwork();
        services
            .AddHealthChecks()
            .AddUrlGroup(new Uri(_configuration.GetValue<string>("Email_HealthUrl")), name: "EmailService")
            .AddSqlServer(_configuration.GetValue<string>("Db_DatabaseContext"), name: "SQLServer")
            .AddAzureBlobStorage(_configuration.GetValue<string>("AZ_Storage_ConnectionString"), name: "AzureStorage");
    }

    /// <summary>
    /// Configure.
    /// </summary>
    /// <param name="builder">Application builder.</param>
    public void Configure(IApplicationBuilder builder)
    {
        builder.UseSerilogRequestLogging();
        builder.UseForwardedHeaders();
        builder.UseResponseCaching();
        builder.ApplyCorsPolicy(_configuration);
        builder.UseMiddleware<Exceptions>();
        builder.UseResponseCompression();
        builder.UseRouting();
        builder.UseAuthentication();
        builder.UseAuthorization();
        builder.SetupSwaggerUi(_configuration, _environment, ApiName, DocVersion);
        builder.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGet("/", context 
                => context.Response.WriteAsync("Sender API"));
        });
        builder.UseHealthChecks("/hc", HealthCheckSupport.WriteResponse());
    }
}