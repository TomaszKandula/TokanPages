namespace TokanPages.WebApi
{
    using System.Net;
    using System.Linq;
    using System.Net.Sockets;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.AspNetCore.HttpOverrides;
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
            Dependencies.Register(services, _configuration, _environment);

            if (_environment.IsDevelopment() || _environment.IsStaging())
                services.SetupSwaggerOptions();

            if (!_environment.IsProduction() && !_environment.IsStaging()) 
                return;

            // Since this app is meant to run in Docker only
            // We get the Docker's internal network IP(s)
            var hostName = Dns.GetHostName();
            var addresses = Dns.GetHostEntry(hostName).AddressList
                .Where(ipAddress => ipAddress.AddressFamily == AddressFamily.InterNetwork)
                .ToList();

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
                options.ForwardLimit = null;
                options.RequireHeaderSymmetry = false;

                foreach (var address in addresses) 
                    options.KnownProxies.Add(address);
            });
        }

        public void Configure(IApplicationBuilder builder)
        {
            builder.UseSerilogRequestLogging();

            builder.UseForwardedHeaders();
            builder.UseHttpsRedirection();
            builder.ApplyCorsPolicy(_configuration);

            builder.UseMiddleware<CustomException>();
            builder.UseMiddleware<CustomCacheControl>();

            builder.UseResponseCompression();
            builder.UseRouting();

            builder.UseAuthentication();
            builder.UseAuthorization();
            builder.UseEndpoints(endpoints => endpoints.MapControllers());

            if (!_environment.IsDevelopment() && !_environment.IsStaging()) 
                return;

            builder.UseSwagger();
            builder.SetupSwaggerUi(_configuration);
        }
    }
}