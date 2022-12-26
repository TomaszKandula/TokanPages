using Microsoft.EntityFrameworkCore;

namespace TokanPages.Persistence.MigrationRunner.Abstractions;

public interface IDatabaseUpdater
{
    void RunAndUpdate<T>(string sourceConnection, string targetConnection) where T : DbContext;
}