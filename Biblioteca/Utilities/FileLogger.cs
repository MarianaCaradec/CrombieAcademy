namespace BibliotecaAPIWeb.Utilities
{
    public class FileLogger
    {
        private readonly string _filePath = "C:\\Users\\maric\\source\\repos\\CrombieAcademy\\Biblioteca\\Logs\\api-errors.log";

        public FileLogger(string filePath)
        {
            _filePath = filePath;
        }

        private void WriteToFile(string logMessage)
        {
            lock (this)
            {
                File.AppendAllText(_filePath, logMessage);
            }
        }

        public void LogError(string message, Exception ex)
        {
            var logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - ERROR: {message}\n"
                + $"Exception: {ex.Message}\nStack Trace: {ex.StackTrace}\n";
            WriteToFile(logMessage);
        }

        public void LogInfo(string message)
        {
            var logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - INFO: {message}\n";
            WriteToFile(logMessage);
        }

    }
}
