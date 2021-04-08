namespace TokanPages.Backend.Shared.Environment
{
    /// <summary>
    /// Allows to setting and getting environmental variables.
    /// It is used when application is bootstrapped in memory for E2E tests.
    /// In such case injected IWebHostEnvironment (Startup.cs) cannot be used.
    /// </summary>
    public static class EnvironmentVariables
    {
        private const string STAGING_VALUE = "Staging";
        private const string STAGING_KEY = "ASPNETCORE_STAGING";

        /// <summary>
        /// Checks whenever staging environment variable is set
        /// when application is running in isolation during E2E testing.
        /// </summary>
        /// <returns>Bool</returns>
        public static bool IsStaging() 
            =>  System.Environment.GetEnvironmentVariable(STAGING_KEY) == STAGING_VALUE;

        /// <summary>
        /// Assigns "STAGING" value to "ASPNETCORE_STAGING" variable that will be
        /// accessible when application is bootstrapped in memory for E2E testing.
        /// </summary>
        /// <returns>Void</returns>
        public static void SetStaging()
            => System.Environment.SetEnvironmentVariable(STAGING_KEY, STAGING_VALUE);
    }
}
