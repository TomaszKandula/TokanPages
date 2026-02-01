using Microsoft.Extensions.Options;
using TokanPages.Backend.Configuration.Options;
using TokanPages.Persistence.DataAccess.Abstractions;

namespace TokanPages.Persistence.DataAccess.Repositories;

public abstract class RepositoryPattern
{
    protected readonly IDbOperations DbOperations;

    protected readonly AppSettingsModel AppSettings;

    protected RepositoryPattern(IDbOperations dbOperations, IOptions<AppSettingsModel> appSettings)
    {
        DbOperations = dbOperations;
        AppSettings = appSettings.Value;
    }
}