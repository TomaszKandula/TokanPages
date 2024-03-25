using System.Net.Http.Headers;
using TokanPages.Gateway.Models;
using TokanPages.Gateway.Services;
using TokanPages.Gateway.Services.Abstractions;

namespace TokanPages.Gateway.Extensions;

/// <summary>
/// 
/// </summary>
public static class ServiceCollection
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
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
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
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