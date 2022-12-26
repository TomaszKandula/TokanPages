using Microsoft.EntityFrameworkCore;

namespace TokanPages.Persistence.MigrationRunner.Abstractions;

public interface IDatabaseCopier
{
    void RunAndUpdate<T>(string sourceConnection, string targetConnection) where T : DbContext;
}