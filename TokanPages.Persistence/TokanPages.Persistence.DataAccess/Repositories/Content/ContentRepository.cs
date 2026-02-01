using TokanPages.Backend.Configuration.Options;
using TokanPages.Persistence.DataAccess.Abstractions;

namespace TokanPages.Persistence.DataAccess.Repositories.Content;

public class ContentRepository : RepositoryPattern, IContentRepository
{
    public ContentRepository(IDbOperations dbOperations, AppSettingsModel appSettings) : base(dbOperations,  appSettings)
    {
    }
}