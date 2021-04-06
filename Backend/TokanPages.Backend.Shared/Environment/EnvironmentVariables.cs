namespace TokanPages.Backend.Shared.Environment
{
    /// <summary>
    /// Allows to setting and getting environmental variables.
    /// It is used when application is bootstrapped in memory for E2E tests.
    /// In such case injected IWebHostEnvironment (Startup.cs) cannot be used.
    /// </summary>
    public static class EnvironmentVariables
    {
        private const string StagingValue = "Staging";
        private const string StagingKey = "ASPNETCORE_STAGING";

        /// <summary>
        /// Checks whenever staging environment variable is set
        /// when application is running in isolation during E2E testing.
        /// </summary>
        /// <returns>Bool</returns>
        public static bool IsStaging() 
        {
            return System.Environment.GetEnvironmentVariable("ASPNETCORE_STAGING") == StagingValue;
        }

        /// <summary>
        /// Assigns "STAGING" value to "ASPNETCORE_STAGING" variable that will be
        /// accessible when application is bootstrapped in memory for E2E testing.
        /// </summary>
        /// <returns>Void</returns>
        public static void SetStaging()
        {
            System.Environment.SetEnvironmentVariable(StagingKey, StagingValue);
        }
    }
}
