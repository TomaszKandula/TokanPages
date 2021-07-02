namespace TokanPages.Backend.Core.Logger
{
    /// <summary>
    /// Logger service that allows to store messages from application.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Debug information to log storage with current datetime stamp.
        /// </summary>
        /// <param name="AMessage"></param>
        void LogDebug(string AMessage);

        /// <summary>
        /// Error message to log storage with current datetime stamp.
        /// </summary>
        /// <param name="AMessage"></param>
        void LogError(string AMessage);

        /// <summary>
        /// Information message to log storage with current datetime stamp.
        /// </summary>
        /// <param name="AMessage"></param>
        void LogInformation(string AMessage);

        /// <summary>
        /// Warning message to log storage with current datetime stamp.
        /// </summary>
        /// <param name="AMessage"></param>
        void LogWarning(string AMessage);

        /// <summary>
        /// Fatal error information to log storage with current datetime stamp.
        /// </summary>
        /// <param name="AMessage"></param>
        void LogCriticalError(string AMessage);
    }
}
