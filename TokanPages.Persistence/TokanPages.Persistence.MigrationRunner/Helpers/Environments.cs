using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.MigrationRunner.Helpers;

[ExcludeFromCodeCoverage]
public static class Environments
{
    public static string CurrentValue => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Testing";

    public static bool IsTesting => CurrentValue == "Testing";

    public static bool IsStaging => CurrentValue == "Staging";

    public static bool IsTestingOrStaging => IsTesting || IsStaging;
}