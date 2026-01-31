using TokanPages.Gateway.Models;

namespace TokanPages.Gateway.Extensions;

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
    public static GatewaySettingsModel GetGatewaySettings(this IConfiguration configuration)
    {
        var settings = new GatewaySettingsModel();
        configuration.Bind(GatewaySettingsModel.SectionName, settings);
        return settings;
    }
}