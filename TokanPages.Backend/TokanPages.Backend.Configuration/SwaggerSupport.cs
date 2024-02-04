using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace TokanPages.Backend.Configuration;

/// <summary>
/// Swagger support.
/// </summary>
[ExcludeFromCodeCoverage]
public static class SwaggerSupport
{
    private const string ApiDocVersion = "v1";

    private const string ApiDocName = "Tokan Pages API";

    private const string XmlFileForWebApi = "TokanPages.WebApi.xml";

    private const string XmlFileForDtoModels = "TokanPages.WebApi.Dto.xml";

    private const string AuthorizationScheme = "Bearer";

    /// <summary>
    /// Setup Swagger options (security and documentation).
    /// </summary>
    /// <param name="services">Service collection.</param>
    /// <param name="environment">Host environment instance.</param>
    public static void SetupSwaggerOptions(this IServiceCollection services, IHostEnvironment environment)
    {
        if (environment.IsProduction())
            return;

        var xmlFileForWebApi = Path.Combine(AppContext.BaseDirectory, XmlFileForWebApi);
        var xmlFileForDtoModels = Path.Combine(AppContext.BaseDirectory, XmlFileForDtoModels);

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc(ApiDocVersion, new OpenApiInfo
            {
                Title = ApiDocName, 
                Version = ApiDocVersion
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

    /// <summary>
    /// Configure Swagger UI.
    /// </summary>
    /// <param name="builder">ApplicationBuilder instance.</param>
    /// <param name="configuration">Provided configuration.</param>
    /// <param name="environment">Host environment instance.</param>
    public static void SetupSwaggerUi(this IApplicationBuilder builder, IConfiguration configuration, IHostEnvironment environment)
    {
        if (environment.IsProduction())
            return;

        builder.UseSwagger();
        builder.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint($"/swagger/{ApiDocVersion}/swagger.json", ApiDocName);
            options.OAuthAppName(ApiDocName);
            options.OAuthClientSecret(configuration.GetValue<string>("Ids_WebSecret"));
        });
    }
}