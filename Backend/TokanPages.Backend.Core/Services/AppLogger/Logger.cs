using Serilog;

namespace TokanPages.Backend.Core.Services.AppLogger
{

    public class Logger : ILogger
    {

        public virtual void LogDebug(string AMessage)
        {
            Log.Debug(AMessage);
        }

        public virtual void LogInfo(string AMessage)
        {
            Log.Information(AMessage);
        }

        public virtual void LogWarn(string AMessage)
        {
            Log.Warning(AMessage);
        }

        public virtual void LogError(string AMessage)
        {
            Log.Error(AMessage);
        }

        public virtual void LogFatality(string AMessage)
        {
            Log.Fatal(AMessage);
        }

    }

}
