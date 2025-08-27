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

        private static string GetHomeDir()
        {
            var home = Environment.GetEnvironmentVariable("HOME");
            if (!string.IsNullOrWhiteSpace(home))
                return home;

            var userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            if (!string.IsNullOrWhiteSpace(userProfile))
                return userProfile;

            var personal = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            if (!string.IsNullOrWhiteSpace(personal) && personal.EndsWith(Path.DirectorySeparatorChar + "Documents"))
                personal = personal[..personal.LastIndexOf(Path.DirectorySeparatorChar)];
            return personal;
        }

        private static string GetAppDataDir()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                var roaming = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                return Path.Combine(roaming, "Quiz App");
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                var home = GetHomeDir();
                return Path.Combine(home, "Library", "Application Support", "Quiz App");
            }

            var xdg = Environment.GetEnvironmentVariable("XDG_CONFIG_HOME");
            if (!string.IsNullOrWhiteSpace(xdg))
                return Path.Combine(xdg, "Quiz App");

            var homeDir = GetHomeDir();
            return Path.Combine(homeDir, ".config", "Quiz App");
        }

        private static string AppDataDir => GetAppDataDir();
        private static string FilePath => Path.Combine(AppDataDir, FileName);

        private static string LegacyAppLocal => Path.Combine(AppContext.BaseDirectory, FileName);

        private static string LegacyMacDocumentsPath
        {
            get
            {
                var personal = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                return Path.Combine(personal, "Library", "Application Support", "Quiz App", FileName);
            }
        }

        public static UserSettings Load()
        {
            try
            {
                Directory.CreateDirectory(AppDataDir);

                if (File.Exists(LegacyAppLocal) && !File.Exists(FilePath))
                {
                    try { File.Copy(LegacyAppLocal, FilePath, overwrite: false); } catch { }
                }

                if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    if (File.Exists(LegacyMacDocumentsPath) && !File.Exists(FilePath))
                    {
                        try { File.Copy(LegacyMacDocumentsPath, FilePath, overwrite: false); } catch { }
                    }
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