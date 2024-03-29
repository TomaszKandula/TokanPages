using Microsoft.EntityFrameworkCore;

namespace TokanPages.Persistence.MigrationRunner.Abstractions;

public interface IDatabaseCopier
{
    Task RunAndCopy<T>(string sourceConnection, string targetConnection) where T : DbContext;
}