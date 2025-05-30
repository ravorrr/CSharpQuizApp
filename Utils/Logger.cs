using System;
using System.IO;

namespace CSharpQuizApp.Utils
{
    public static class Logger
    {
        private static readonly string LogFilePath =
            Path.Combine(AppContext.BaseDirectory, "logs", "errors.txt");

        public static void LogError(Exception ex)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(LogFilePath));
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