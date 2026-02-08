namespace TokanPages.Backend.UtilityService.Abstractions;

public interface ILoggerService
{
    void LogDebug(string message);

    void LogError(string message);

    void LogInformation(string message);

    void LogWarning(string message);

    void LogFatal(string message);
}