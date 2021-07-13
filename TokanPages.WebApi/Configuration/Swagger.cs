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
        public static void SetupSwaggerOptions(IServiceCollection AServices)
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

        public static void SetupSwaggerUi(IApplicationBuilder ABuilder, IConfiguration AConfiguration)
        {
            ABuilder.UseSwaggerUI(AOption =>
            {
                AOption.SwaggerEndpoint("/swagger/v1/swagger.json", "TokanPages API");
                AOption.OAuthAppName("TokanPages");
                AOption.OAuthClientSecret(AConfiguration.GetSection("IdentityServer")["WebSecret"]);
            });
        }
    }
}