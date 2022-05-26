namespace TokanPages.WebApi.Configuration;

using System;
using System.IO;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[ExcludeFromCodeCoverage]
public static class SwaggerSupport
{
    private const string ApiVersion = "v1";

    private const string ApiName = "Tokan Pages API";

    private const string XmlFileForWebApi = "TokanPages.WebApi.xml";

    private const string XmlFileForDtoModels = "TokanPages.Backend.Dto.xml";

    private const string AuthorizationScheme = "Bearer";

    public static void SetupSwaggerOptions(this IServiceCollection services, IHostEnvironment environment)
    {
        if (environment.IsProduction())
            return;

        var xmlFileForWebApi = Path.Combine(AppContext.BaseDirectory, XmlFileForWebApi);
        var xmlFileForDtoModels = Path.Combine(AppContext.BaseDirectory, XmlFileForDtoModels);

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc(ApiVersion, new OpenApiInfo
            {
                Title = ApiName, 
                Version = ApiVersion
            });

            options.EnableAnnotations();
            options.IncludeXmlComments(xmlFileForWebApi);
            options.IncludeXmlComments(xmlFileForDtoModels);

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

    public static void SetupSwaggerUi(this IApplicationBuilder builder, IConfiguration configuration, IHostEnvironment environment)
    {
        if (environment.IsProduction())
            return;

        builder.UseSwagger();
        builder.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint($"/swagger/{ApiVersion}/swagger.json", ApiName);
            options.OAuthAppName(ApiName);
            options.OAuthClientSecret(configuration.GetSection("IdentityServer")["WebSecret"]);
        });
    }
}