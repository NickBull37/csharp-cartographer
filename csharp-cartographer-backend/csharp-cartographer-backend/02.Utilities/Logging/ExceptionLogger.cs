namespace csharp_cartographer_backend._02.Utilities.Logging
{
    public static class ExceptionLogger
    {
        private static readonly string _logFilePath = @"C:\Users\nbuli\source\repos\csharp-cartographer-be\csharp-cartographer-be\csharp-cartographer-be\02.Utilities\Logging\Logs\ExceptionLog.txt";

        public static void LogException(Exception ex, string location)
        {
            // ClearLogFile();

            LogMessage($"============================================================");
            LogMessage(" ");
            LogMessage($"Location: {location}");
            LogMessage(" ");
            LogMessage($"Time: {DateTime.Now}");
            LogMessage(" ");
            LogMessage(ex.Message);
            LogMessage(" ");
            LogMessage($"============================================================");
            LogMessage(" ");
        }

        private static void LogMessage(string message)
        {
            using StreamWriter writer = new(_logFilePath, true);
            writer.WriteLine($"{message}");
        }

        private static void ClearLogFile()
        {
            using StreamWriter writer = new(_logFilePath, false);
            writer.Write("");
        }
    }
}
