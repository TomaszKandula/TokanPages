namespace TokanPages.Backend.Shared.Environment
{

    /// <summary>
    /// Allows to setting and getting environemntal variables.
    /// It is used when application is bootstrapped in memory for E2E tests.
    /// In such case injected IWebHostEnvironment (Startup.cs) cannot be used.
    /// </summary>
    public static class EnvironmentVariables
    {

        private const string StagingValue = "Staging";
        private const string StagingKey = "ASPNETCORE_STAGING";

        public static bool IsStaging() 
        {
            return System.Environment.GetEnvironmentVariable("ASPNETCORE_STAGING") == StagingValue;
        }

        public static void SetStaging()
        {
            System.Environment.SetEnvironmentVariable(StagingKey, StagingValue);
        }

    }

}
