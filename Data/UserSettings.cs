using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace CSharpQuizApp.Data
{
    public class UserSettings
    {
        public string PlayerName { get; set; } = "Unknown";
        public string Language { get; set; } = "pl-PL";

        private const string FileName = "usersettings.json";

        private static string GetAppDataDir()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                var roaming = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                return Path.Combine(roaming, "Quiz App");
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                var home = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                return Path.Combine(home, "Library", "Application Support", "Quiz App");
            }
            
            var homeDir = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(homeDir, ".config", "Quiz App");
        }

        private static string AppDataDir => GetAppDataDir();
        private static string FilePath => Path.Combine(AppDataDir, FileName);
        
        private static string LegacyPath => Path.Combine(AppContext.BaseDirectory, FileName);

        public static UserSettings Load()
        {
            try
            {
                Directory.CreateDirectory(AppDataDir);
                
                if (File.Exists(LegacyPath) && !File.Exists(FilePath))
                {
                    try
                    {
                        File.Copy(LegacyPath, FilePath, overwrite: false);
                    }
                    catch { }
                }

                if (!File.Exists(FilePath))
                    return new UserSettings();

                var json = File.ReadAllText(FilePath);
                return JsonSerializer.Deserialize<UserSettings>(json) ?? new UserSettings();
            }
            catch
            {
                return new UserSettings();
            }
        }

        public void Save()
        {
            try
            {
                Directory.CreateDirectory(AppDataDir);
                var json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(FilePath, json);
#if DEBUG
                Console.WriteLine($"[DEBUG] UserSettings saved → {FilePath}");
#endif
            }
            catch
            {
#if DEBUG
                Console.WriteLine($"[DEBUG] Failed to save UserSettings at {FilePath}");
#endif
            }
        }
    }
}