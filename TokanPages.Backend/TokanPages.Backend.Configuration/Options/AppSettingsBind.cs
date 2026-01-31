using Microsoft.Extensions.Configuration;

namespace TokanPages.Backend.Configuration.Options;

public static class AppSettingsBind
{
    public static AppSettings GetAppSettings(IConfiguration configuration)
    {
        var settings = new AppSettings();
        configuration.Bind(AppSettings.SectionName, settings);
        return settings;
    }
}