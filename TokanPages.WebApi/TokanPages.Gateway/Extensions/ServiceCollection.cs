using System.Net.Http.Headers;
using TokanPages.Gateway.Models;
using TokanPages.Gateway.Services;
using TokanPages.Gateway.Services.Abstractions;

namespace TokanPages.Gateway.Extensions;

/// <summary>
/// Service collection.
/// </summary>
public static class ServiceCollection
{
    /// <summary>
    /// Adds proxy HTTP client(s) to a service collection.
    /// </summary>
    /// <param name="services">Service collection to be extended.</param>
    public static void AddProxyHttpClient(this IServiceCollection services)
    {
        services.AddHttpClient<IProxyHttpClient, ProxyHttpClient>(client =>
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue
            {
                NoCache = true,
                NoStore = true,
                MaxAge = new TimeSpan(0),
                MustRevalidate = true
            };
        }).ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
        {
            AllowAutoRedirect = false,
            UseProxy = false,
            UseCookies = false
        });
    }

    /// <summary>
    /// Adds named HTTP client(s) to a service.
    /// </summary>
    /// <param name="services">Service collection to be extended.</param>
    /// <param name="configuration">Passed configuration.</param>
    public static void AddNamedHttpClients(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = new GatewaySettings();
        configuration.Bind(settings);

        var ports = settings.Routes?
            .Select(routeDefinition => routeDefinition.Port)
            .Distinct();

        foreach (var port in ports!)
        {
            var url = $"{settings.Defaults?.Schema}://{settings.Defaults?.Host}:{port}";
            services.AddHttpClient(port, client => client.BaseAddress = new Uri(url));
        }
    }
}