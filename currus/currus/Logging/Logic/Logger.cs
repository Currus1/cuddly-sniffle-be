namespace currus.Logging.Logic
{
    public static class Logger
    {
        static string logPath = Directory.GetCurrentDirectory() + "/Logging/Logs/" + DateTime.Now.ToString("yyyy-M-dd") + "-log.txt";
        static string logFolderPath = Directory.GetCurrentDirectory() + "/Logging/Logs";

        public static void createLogFile()
        {
            Directory.CreateDirectory(logFolderPath);
            if (!File.Exists(logPath))
            {
                var file = File.Create(logPath);
                file.Close();
            }
               
        }

        public static void LogInfo(string message)
        {
            using (StreamWriter writer = new StreamWriter(logPath, true))
            {
                writer.WriteLine($"{DateTime.Now} [INFO]: {message}");
            }
        }

        public static void LogError(string error)
        {
            using (StreamWriter writer = new StreamWriter(logPath, true))
            {
                writer.WriteLine($"{DateTime.Now} [ERROR]: {error}");
            }
        }
    }
}
