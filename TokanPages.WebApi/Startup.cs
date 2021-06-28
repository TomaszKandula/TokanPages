using System;
using System.Net;
using System.Linq;
using System.Net.Sockets;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.ResponseCompression;
using TokanPages.WebApi.Middleware;
using TokanPages.WebApi.Configuration;
using Serilog;

namespace TokanPages.WebApi
{
    [ExcludeFromCodeCoverage]
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
            AServices.AddControllers();        
            AServices.AddResponseCompression(AOptions => AOptions.Providers.Add<GzipCompressionProvider>());
            Dependencies.Register(AServices, FConfiguration, FEnvironment);
            
            if (FEnvironment.IsDevelopment())
                SetupSwaggerOptions(AServices);

            if (!FEnvironment.IsProduction() && !FEnvironment.IsStaging()) 
                return;
                
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

        public void Configure(IApplicationBuilder ABuilder)
        {
            ABuilder.UseSerilogRequestLogging();

            ABuilder.UseMiddleware<CustomCors>();
            ABuilder.UseMiddleware<CustomException>();
            
            ABuilder.UseHttpsRedirection();
            ABuilder.UseForwardedHeaders();
            ABuilder.UseResponseCompression();

            ABuilder.UseRouting();
            ABuilder.UseAuthentication();
            ABuilder.UseAuthorization();
            ABuilder.UseEndpoints(AEndpoints => AEndpoints.MapControllers());

            if (!FEnvironment.IsDevelopment()) 
                return;
            
            ABuilder.UseSwagger();
            SetupSwaggerUi(ABuilder);
        }

        private static void SetupSwaggerOptions(IServiceCollection AServices)
        {
            AServices.AddSwaggerGen(AOption =>
            {
                AOption.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "TokanPages API", 
                    Version = "v1"
                });
                
                AOption.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Please provide JWT",
                    BearerFormat = "JWT",
                    Scheme = "bearer",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                
                AOption.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference 
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
        }

        private void SetupSwaggerUi(IApplicationBuilder ABuilder)
        {
            ABuilder.UseSwaggerUI(AOption =>
            {
                AOption.SwaggerEndpoint("/swagger/v1/swagger.json", "TokanPages API");
                AOption.OAuthAppName("TokanPages");
                AOption.OAuthClientSecret(FConfiguration.GetSection("IdentityServer")["WebSecret"]);
            });
        }
    }
}
