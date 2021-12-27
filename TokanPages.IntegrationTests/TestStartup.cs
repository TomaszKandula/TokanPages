namespace TokanPages.IntegrationTests;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApi;
using Backend.Database;
using WebApi.Middleware;
using WebApi.Configuration;
using Backend.Core.Exceptions;

public class TestStartup
{
    private readonly IConfiguration _configuration;

    public TestStartup(IConfiguration configuration) => _configuration = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCors();
        services.AddMvc().AddApplicationPart(typeof(Startup).Assembly);
        services.AddControllers();
        services.AddApiVersioning(options =>
        {
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ErrorResponses = new ApiVersionException();
        });

        SetupTestDatabase(services);
        services.CommonServices(_configuration);
    }

    public void Configure(IApplicationBuilder builder)
    {
        builder.UseForwardedHeaders();
        builder.UseHttpsRedirection();
        builder.ApplyCorsPolicy(_configuration);
        builder.UseMiddleware<Exceptions>();
        builder.UseRouting();
        builder.UseAuthentication();
        builder.UseAuthorization();
        builder.UseMiddleware<TokenControl>();
        builder.UseEndpoints(endpoints => endpoints.MapControllers());
    }

    private void SetupTestDatabase(IServiceCollection services)
    {
        services.AddDbContext<DatabaseContext>(options =>
        {
            options.UseSqlServer(_configuration.GetConnectionString("DbConnectTest"));
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
            options.EnableSensitiveDataLogging();
        });
    }
}