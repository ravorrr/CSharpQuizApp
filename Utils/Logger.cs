using System;
using System.IO;

namespace CSharpQuizApp.Utils
{
    public static class Logger
    {
        private const string LogFilePath = "error.log";

        public static void LogError(Exception ex)
        {
            try
            {
                using var writer = new StreamWriter(LogFilePath, append: true);
                writer.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {ex.Message}");
                writer.WriteLine(ex.StackTrace);
                writer.WriteLine("--------------------------------------------------");
            }
            catch
            {
                // ignorujemy błąd logowania
            }
        }
    }
}