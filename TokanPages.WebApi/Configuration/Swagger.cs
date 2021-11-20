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
        private const string APiVersion = "v1";

        private const string ApiName = "TokanPages API";
        
        private const string AuthorizationScheme = "Bearer";
        
        public static void SetupSwaggerOptions(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(APiVersion, new OpenApiInfo
                {
                    Title = ApiName, 
                    Version = APiVersion
                });
                
                options.AddSecurityDefinition(AuthorizationScheme, new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Please provide JWT",
                    BearerFormat = "JWT",
                    Scheme = AuthorizationScheme.ToLower(),
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
                                Id = AuthorizationScheme,
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
        }

        public static void SetupSwaggerUi(this IApplicationBuilder builder, IConfiguration configuration)
        {
            builder.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint($"/swagger/{APiVersion}/swagger.json", ApiName);
                options.OAuthAppName(ApiName);
                options.OAuthClientSecret(configuration.GetSection("IdentityServer")["WebSecret"]);
            });
        }
    }
}