namespace currus.Logging.Logic
{
    public static class Logger
    {
        static string logPath = Directory.GetCurrentDirectory() + "/Logging/Logs/minute-" + DateTime.Now.ToString("mm") + "-log.txt";
        
        public static void LogInfo(string message)
        {
            using (StreamWriter writer = new StreamWriter(logPath, false))
            {
                writer.WriteLine($"{DateTime.Now} [INFO]: {message}");
            }
        }

        public static void LogError(string error)
        {
            using (StreamWriter writer = new StreamWriter(logPath, false))
            {
                writer.WriteLine($"{DateTime.Now} [ERROR]: {error}");
            }
        }
    }
}
