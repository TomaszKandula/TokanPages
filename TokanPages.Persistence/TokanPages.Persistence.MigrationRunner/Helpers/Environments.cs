namespace TokanPages.Persistence.MigrationRunner.Helpers;

public static class Environments
{
    private static readonly string? EnvironmentValue = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

    private static readonly bool IsTesting = EnvironmentValue == "Testing";

    private static readonly bool IsStaging = EnvironmentValue == "Staging";

    public static bool IsTestingOrStaging => IsTesting || IsStaging;
}