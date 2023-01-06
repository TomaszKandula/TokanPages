namespace TokanPages.Persistence.MigrationRunner.Helpers;

public static class DatabaseUpdate
{
    private const string Directory = "Resources";

    private const string MigrationScriptNameTemplate = "{VERSION_NUMBER}_{CONTEXT_NAME}_ToProd.sql";

    public const string DefaultUserScript = "CreateDbUser.sql";

    public static string BuildMigrationScriptName(int version, string contextName)
    {
        return MigrationScriptNameTemplate
            .Replace("{VERSION_NUMBER}", version.ToString())
            .Replace("{CONTEXT_NAME}", contextName);
    }

    public static bool HasSqlScript(string scriptName)
    {
        return File.Exists(Path.Combine(Directory, scriptName));
    }

    public static string GetSqlScript(string scriptName)
    {
        return File.ReadAllText(Path.Combine(Directory, scriptName));
    }
}