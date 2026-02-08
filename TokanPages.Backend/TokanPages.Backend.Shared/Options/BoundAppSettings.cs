using Microsoft.Extensions.Configuration;

namespace TokanPages.Backend.Shared.Options;

public static class BoundAppSettings
{
    public static AppSettingsModel GetAppSettings(this IConfiguration configuration)
    {
        var settings = new AppSettingsModel();
        configuration.Bind(AppSettingsModel.SectionName, settings);
        return settings;
    }
}