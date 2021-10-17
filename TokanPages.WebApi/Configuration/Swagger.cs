namespace TokanPages.WebApi.Configuration
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.OpenApi.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    [ExcludeFromCodeCoverage]
    public static class Swagger
    {
        public static void SetupSwaggerOptions(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "TokanPages API", 
                    Version = "v1"
                });
                
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Please provide JWT",
                    BearerFormat = "JWT",
                    Scheme = "bearer",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
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

        public static void SetupSwaggerUi(IApplicationBuilder builder, IConfiguration configuration)
        {
            builder.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "TokanPages API");
                options.OAuthAppName("TokanPages");
                options.OAuthClientSecret(configuration.GetSection("IdentityServer")["WebSecret"]);
            });
        }
    }
}