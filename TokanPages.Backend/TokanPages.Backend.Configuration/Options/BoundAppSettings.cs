using Microsoft.Extensions.Configuration;

namespace TokanPages.Backend.Configuration.Options;

public static class BoundAppSettings
{
    public static AppSettings GetSettings(this IConfiguration configuration)
    {
        var settings = new AppSettings();
        configuration.Bind(AppSettings.SectionName, settings);
        return settings;
    }
}