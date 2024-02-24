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
    /// <summary>
    /// Setup Swagger options (security and documentation).
    /// </summary>
    /// <param name="services">Service collections.</param>
    /// <param name="environment">Host environment instance.</param>
    /// <param name="apiName">API name to be displayed in Swagger UI.</param>
    /// <param name="docVersion">Document version to be displayed in Swagger UI.</param>
    /// <param name="xmls">List of projects that should have XML documentation.</param>
    public static void SetupSwaggerOptions(this IServiceCollection services, IHostEnvironment environment, string apiName, string docVersion, string[]? xmls)
    {
        if (environment.IsProduction())
            return;

        services.AddSwaggerGen(options =>
        {
            options.EnableAnnotations();
            options.SwaggerDoc(docVersion, new OpenApiInfo
            {
                Title = apiName, 
                Version = docVersion
            });

            if (xmls is not null)
            {
                foreach (var item in xmls)
                {
                    var xmlDoc = Path.Combine(AppContext.BaseDirectory, item);
                    options.IncludeXmlComments(xmlDoc);
                }
            }

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

    /// <summary>
    /// Configure Swagger UI.
    /// </summary>
    /// <param name="builder">Application builder instance.</param>
    /// <param name="configuration">Application configuration instance.</param>
    /// <param name="environment">Host environment instance.</param>
    /// <param name="apiName">API name to be displayed in Swagger UI.</param>
    /// <param name="docVersion">Document version to be displayed in Swagger UI.</param>
    public static void SetupSwaggerUi(this IApplicationBuilder builder, IConfiguration configuration, IHostEnvironment environment, string apiName, string docVersion)
    {
        if (environment.IsProduction())
            return;

        builder.UseSwagger();
        builder.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint($"/swagger/{docVersion}/swagger.json", apiName);
            options.OAuthAppName(apiName);
            options.OAuthClientSecret(configuration.GetSection("IdentityServer")["WebSecret"]);
        });
    }
}