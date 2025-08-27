using System;
using System.IO;
using System.Text.Json;

namespace CSharpQuizApp.Data
{
    public class UserSettings
    {
        public string PlayerName { get; set; } = "Unknown";
        public string Language { get; set; } = "pl-PL";

        // ~/AppData/Roaming/CSharpQuizApp (Windows), ~/Library/Application Support/CSharpQuizApp (macOS), ~/.config/CSharpQuizApp (Linux)
        private static readonly string AppDataDir =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                "..", "Library", "Application Support", "CSharpQuizApp");
        private static readonly string FilePath = Path.Combine(AppDataDir, "usersettings.json");

        public static UserSettings Load()
        {
            try
            {
                Directory.CreateDirectory(AppDataDir);
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
            }
            catch
            {
                // cicho ignorujemy błędy IO
            }
        }
    }
}