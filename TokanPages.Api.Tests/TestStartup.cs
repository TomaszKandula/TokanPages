using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TokanPages.Api.Middleware;
using TokanPages.Api.Configuration;
using TokanPages.Backend.Database;

namespace TokanPages.Api.Tests
{
    public class TestStartup
    {
        private readonly IConfiguration FConfiguration;

        public TestStartup(IConfiguration AConfiguration) => FConfiguration = AConfiguration;

        public void ConfigureServices(IServiceCollection AServices)
        {
            AServices.AddMvc().AddApplicationPart(typeof(Startup).Assembly);
            AServices.AddControllers();

            SetupTestDatabase(AServices);
            Dependencies.CommonServices(AServices, FConfiguration);
        }

        public static void Configure(IApplicationBuilder AApplication)
        {
            AApplication.UseMiddleware<CustomCors>();
            AApplication.UseMiddleware<CustomException>();
            AApplication.UseForwardedHeaders();
            AApplication.UseHttpsRedirection();
            AApplication.UseRouting();
            AApplication.UseEndpoints(AEndpoints => AEndpoints.MapControllers());
        }

        private void SetupTestDatabase(IServiceCollection AServices)
        {
            AServices.AddDbContext<DatabaseContext>(AOptions =>
            {
                AOptions.UseSqlServer(FConfiguration.GetConnectionString("DbConnectTest"));
                AOptions.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
                AOptions.EnableSensitiveDataLogging();
            });
        }
    }
}