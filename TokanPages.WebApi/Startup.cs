namespace TokanPages.WebApi
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.ResponseCompression;
    using Newtonsoft.Json.Converters;
    using Middleware;
    using Configuration;
    using Serilog;

    [ExcludeFromCodeCoverage]
    public class Startup
    {
        private readonly IConfiguration _configuration;

        private readonly IHostEnvironment _environment;

        public Startup(IConfiguration configuration, IHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.Converters.Add(new StringEnumConverter()));
            services.AddResponseCompression(options => options.Providers.Add<GzipCompressionProvider>());
            services.RegisterDependencies(_configuration, _environment);
            services.SetupSwaggerOptions(_environment);
            services.SetupDockerInternalNetwork();
        }

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
            builder.UseEndpoints(endpoints => endpoints.MapControllers());

            builder.SetupSwaggerUi(_configuration, _environment);
        }
    }
}