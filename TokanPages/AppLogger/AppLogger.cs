using Serilog;

namespace TokanPages.AppLogger
{

    public class AppLogger : IAppLogger
    {

        public void LogDebug(string AMessage)
        {
            Log.Debug(AMessage);
        }

        public void LogInfo(string AMessage)
        {
            Log.Information(AMessage);
        }

        public void LogWarn(string AMessage)
        {
            Log.Warning(AMessage);
        }

        public void LogError(string AMessage)
        {
            Log.Error(AMessage);
        }

        public void LogFatality(string AMessage)
        {
            Log.Fatal(AMessage);
        }

    }

}
