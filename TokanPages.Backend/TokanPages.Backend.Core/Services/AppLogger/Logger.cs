using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Serilog;

namespace TokanPages.Backend.Core.Services.AppLogger
{
    [ExcludeFromCodeCoverage]
    public sealed class Logger : ILogger
    {
        public void LogDebug(string AMessage)
        {
            Debug.WriteLine($"[{nameof(Logger)} output]: {AMessage}");
            Log.Debug("{AMessage}", AMessage);
        }

        public void LogInformation(string AMessage)
        {
            Debug.WriteLine($"[{nameof(Logger)} output]: {AMessage}");
            Log.Information("{AMessage}", AMessage);
        }

        public void LogWarning(string AMessage)
        {
            Debug.WriteLine($"[{nameof(Logger)} output]: {AMessage}");
            Log.Warning("{AMessage}", AMessage);
        }

        public void LogError(string AMessage)
        {
            Debug.WriteLine($"[{nameof(Logger)} output]: {AMessage}");
            Log.Error("{AMessage}", AMessage);
        }

        public void LogCriticalError(string AMessage)
        {
            Debug.WriteLine($"[{nameof(Logger)} output]: {AMessage}");
            Log.Fatal("{AMessage}", AMessage);
        }
    }
}
