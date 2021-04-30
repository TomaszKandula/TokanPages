using System.Net;
using System.Linq;
using System.Net.Sockets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.ResponseCompression;
using TokanPages.Middleware;
using TokanPages.Configuration;
using TokanPages.CustomHandlers;
using TokanPages.Backend.Shared.Settings;
using TokanPages.Backend.Shared.Environment;
using TokanPages.Backend.Database.Initialize;
using Serilog;

namespace TokanPages
{
    public class Startup
    {
        private readonly IConfiguration FConfiguration;
        private readonly IWebHostEnvironment FEnvironment;

        public Startup(IConfiguration AConfiguration, IWebHostEnvironment AEnvironment)
        {
            FConfiguration = AConfiguration;
            FEnvironment = AEnvironment;
        }

        public void ConfigureServices(IServiceCollection AServices)
        {
            AServices
                .AddMvc(AOption => AOption.CacheProfiles
                .Add("Standard", new CacheProfile
                { 
                    Duration = 10, 
                    Location = ResponseCacheLocation.Any, 
                    NoStore = false 
                }));

            AServices.AddControllers();          
            AServices.AddSpaStaticFiles(AOptions => AOptions.RootPath = "ClientApp/build");
            AServices.AddResponseCompression(AOptions => AOptions.Providers.Add<GzipCompressionProvider>());

            // For E2E testing only when application is bootstrapped in memory
            if (EnvironmentVariables.IsStaging())
                Dependencies.RegisterForTests(AServices, FConfiguration);
            
            // Local development
            if (FEnvironment.IsDevelopment())
            {
                Dependencies.RegisterForDevelopment(AServices, FConfiguration);

                AServices.AddSwaggerGen(AOption =>
                    AOption.SwaggerDoc("v1", new OpenApiInfo { Title = "TokanPagesApi", Version = "v1" }));
            }

            // Production and Staging (deployment slots only)
            if (!FEnvironment.IsProduction() && !FEnvironment.IsStaging()) 
                return;
            
            Dependencies.Register(AServices, FConfiguration);
                
            // Since this app is meant to run in Docker only
            // We get the Docker's internal network IP(s)
            var LHostName = Dns.GetHostName();
            var LAddresses = Dns.GetHostEntry(LHostName).AddressList
                .Where(AIpAddress => AIpAddress.AddressFamily == AddressFamily.InterNetwork)
                .ToList();

            AServices.Configure<ForwardedHeadersOptions>(AOptions =>
            {
                AOptions.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
                AOptions.ForwardLimit = null;
                AOptions.RequireHeaderSymmetry = false;

                foreach (var LAddress in LAddresses) 
                    AOptions.KnownProxies.Add(LAddress);
            });
        }

        public void Configure(IApplicationBuilder AApplication, AppUrls AAppUrls)
        {
            var LScopeFactory = AApplication.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using var LScope = LScopeFactory.CreateScope();
            var LDatabaseInitializer = LScope.ServiceProvider.GetService<IDbInitializer>();

            if (FEnvironment.IsDevelopment() || EnvironmentVariables.IsStaging())
            {
                LDatabaseInitializer.StartMigration();
                LDatabaseInitializer.SeedData();
            }

            if (!EnvironmentVariables.IsStaging())
                AApplication.UseSerilogRequestLogging();
            
            AApplication.UseForwardedHeaders();
            AApplication.UseExceptionHandler(ExceptionHandler.Handle);
            AApplication.UseMiddleware<GarbageCollector>();
            AApplication.UseMiddleware<CustomCors>();

            AApplication.UseResponseCompression();
            AApplication.UseHttpsRedirection();
            AApplication.UseStaticFiles();
            AApplication.UseSpaStaticFiles();
            AApplication.UseRouting();

            if (FEnvironment.IsDevelopment())
            {
                AApplication.UseSwagger();
                AApplication.UseSwaggerUI(AOption =>
                    AOption.SwaggerEndpoint("/swagger/v1/swagger.json", "TokanPagesApi version 1"));
            }

            AApplication.UseEndpoints(AEndpoints => 
                AEndpoints.MapControllers());

            AApplication.UseSpa(ASpa =>
            {
                ASpa.Options.SourcePath = "ClientApp";
                if (FEnvironment.IsDevelopment())
                    ASpa.UseProxyToSpaDevelopmentServer(AAppUrls.DevelopmentOrigin);
            });           
        }
    }
}
