namespace TokanPages.WebApi.Tests
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Backend.Database;
    using Middleware;
    using Configuration;

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

        public static void Configure(IApplicationBuilder ABuilder)
        {
            ABuilder.UseMiddleware<CustomCors>();
            ABuilder.UseMiddleware<CustomException>();
            ABuilder.UseForwardedHeaders();
            ABuilder.UseHttpsRedirection();
            ABuilder.UseRouting();
            ABuilder.UseAuthentication();
            ABuilder.UseAuthorization();
            ABuilder.UseEndpoints(AEndpoints => AEndpoints.MapControllers());
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