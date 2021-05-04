using System.Diagnostics;
using Serilog;

namespace TokanPages.Backend.Core.Services.AppLogger
{
    public sealed class Logger : ILogger
    {
        public void LogDebug(string AMessage)
        {
            Trace.WriteLine($"[Debug Trace]: {AMessage}");
            Log.Debug("{AMessage}", AMessage);
        }

        public void LogInfo(string AMessage)
        {
            Trace.WriteLine($"[Info Trace]: {AMessage}");
            Log.Information("{AMessage}", AMessage);
        }

        public void LogWarn(string AMessage)
        {
            Trace.WriteLine($"[Warning Trace]: {AMessage}");
            Log.Warning("{AMessage}", AMessage);
        }

        public void LogError(string AMessage)
        {
            Trace.WriteLine($"[Error Trace]: {AMessage}");
            Log.Error("{AMessage}", AMessage);
        }

        public void LogFatality(string AMessage)
        {
            Trace.WriteLine($"[Critical Error Trace]: {AMessage}");
            Log.Fatal("{AMessage}", AMessage);
        }
    }
}
