using TokanPages.Gateway.Models;

namespace TokanPages.Gateway.Helpers;

/// <summary>
/// Bound gateway settings.
/// </summary>
public static class BoundGatewaySettings
{
    /// <summary>
    /// Returns mapped gateways settings.
    /// </summary>
    /// <param name="configuration">IConfiguration instance.</param>
    /// <returns>Mapped gateways settings</returns>
    public static GatewaySettings GetSettings(IConfiguration configuration)
    {
        var settings = new GatewaySettings();
        configuration.Bind(GatewaySettings.SectionName, settings);
        return settings;
    }
}