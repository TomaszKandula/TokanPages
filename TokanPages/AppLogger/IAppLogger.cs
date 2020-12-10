namespace TokanPages.AppLogger
{
    /// <summary>
    /// Logger service that allows to store messages from application.
    /// </summary>
    public interface IAppLogger
    {

        /// <summary>
        /// Debug infomration to log storage with current datetime stamp.
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
        void LogInfo(string AMessage);

        /// <summary>
        /// Warning message to log storage with current datetime stamp.
        /// </summary>
        /// <param name="AMessage"></param>
        void LogWarn(string AMessage);

        /// <summary>
        /// Fatal error infomration to log storage with current datetime stamp.
        /// </summary>
        /// <param name="AMessage"></param>
        void LogFatality(string AMessage);

    }

}