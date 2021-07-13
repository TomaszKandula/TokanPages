namespace TokanPages.Backend.Core.Logger
{
    public interface ILogger
    {
        void LogDebug(string AMessage);

        void LogError(string AMessage);

        void LogInformation(string AMessage);

        void LogWarning(string AMessage);

        void LogCriticalError(string AMessage);
    }
}