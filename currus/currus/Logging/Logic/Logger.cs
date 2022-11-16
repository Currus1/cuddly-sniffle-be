namespace currus.Logging.Logic
{
    public static class Logger
    {
        static string logPath = Directory.GetCurrentDirectory() + "/Logging/Logs/" + DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss") + "-log.txt";
        static string logFolderPath = Directory.GetCurrentDirectory() + "/Logging/Logs";

        public static void createLogFile()
        {
            Directory.CreateDirectory(logFolderPath);
            File.Create(logPath);
        }

        public static void LogInfo(string message)
        {
            using (StreamWriter writer = new StreamWriter(logPath, false))
            {
                writer.WriteLine($"{DateTime.Now} [INFO]: {message}");
                writer.Close();
            }
        }

        public static void LogError(string error)
        {
            using (StreamWriter writer = new StreamWriter(logPath, false))
            {
                writer.WriteLine($"{DateTime.Now} [ERROR]: {error}");
                writer.Close();
            }
        }
    }
}
