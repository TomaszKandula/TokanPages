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
        private readonly IConfiguration _configuration;

        public TestStartup(IConfiguration configuration) => _configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddApplicationPart(typeof(Startup).Assembly);
            services.AddControllers();

            SetupTestDatabase(services);
            Dependencies.CommonServices(services, _configuration);
        }

        public static void Configure(IApplicationBuilder builder)
        {
            builder.UseMiddleware<CustomCors>();
            builder.UseMiddleware<CustomException>();
            builder.UseForwardedHeaders();
            builder.UseHttpsRedirection();
            builder.UseRouting();
            builder.UseAuthentication();
            builder.UseAuthorization();
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
}