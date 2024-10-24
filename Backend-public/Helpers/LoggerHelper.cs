using Serilog;
using Serilog.Events;

namespace SportsApp.Helpers
{
    public static class LoggerHelper
    {
        public static void LogInformation(string message)
        {
            Log.Information(message);
        }

        public static void LogWarning(string message)
        {
            Log.Warning(message);
        }

        public static void LogError(string message)
        {
            Log.Error(message);
        }

        public static void LogDebug(string message)
        {
            Log.Debug(message);
        }

        public static void LogFatal(string message)
        {
            Log.Fatal(message);
        }

        public static void LogException(Exception ex, string message)
        {
            Log.Error(ex, message);
        }
    }
}
