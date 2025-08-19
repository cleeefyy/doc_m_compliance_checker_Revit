// In Logger.cs
using System;
using System.IO;

namespace DocM.RevitAddin
{
    public static class Logger
    {
        private static readonly string logDirectory = @"C:\Temp\DocM\logs";
        private static readonly string logFilePath;

        static Logger()
        {
            // Create the directory if it doesn't exist
            Directory.CreateDirectory(logDirectory);
            // Set up a log file name with the current date
            logFilePath = Path.Combine(logDirectory, $"DocM_Log_{DateTime.Now:yyyy-MM-dd}.txt");
        }

        public static void Log(string message)
        {
            try
            {
                // Append the message with a timestamp to the log file
                string logMessage = $"{DateTime.Now:HH:mm:ss} - {message}{Environment.NewLine}";
                File.AppendAllText(logFilePath, logMessage);
            }
            catch (Exception ex)
            {
                // If logging fails, we can't do much, but we can try to debug.
                System.Diagnostics.Debug.WriteLine($"Failed to write to log: {ex.Message}");
            }
        }
    }
}