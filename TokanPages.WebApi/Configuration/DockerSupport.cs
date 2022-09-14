using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Sockets;
using Microsoft.AspNetCore.HttpOverrides;

namespace TokanPages.WebApi.Configuration;

/// <summary>
/// Docker support
/// </summary>
[ExcludeFromCodeCoverage]
public static class DockerSupport
{
    /// <summary>
    /// Adds host IP addresses to known proxies
    /// </summary>
    /// <remarks>
    /// Setup forwarded headers
    /// </remarks>
    /// <param name="services">Service collection</param>
    public static void SetupDockerInternalNetwork(this IServiceCollection services)
    {
        var hostName = Dns.GetHostName();
        var addresses = Dns.GetHostEntry(hostName).AddressList
            .Where(ipAddress => ipAddress.AddressFamily == AddressFamily.InterNetwork)
            .ToList();

        services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            options.ForwardLimit = null;
            options.RequireHeaderSymmetry = false;

            foreach (var address in addresses) 
                options.KnownProxies.Add(address);
        });
    }
}