using System.IO;
using System.Text.Json;

namespace CSharpQuizApp.Utils
{
    public class UserSettings
    {
        private const string SettingsFilePath = "user_settings.json";

        public string PlayerName { get; set; } = "";

        public static UserSettings Load()
        {
            if (File.Exists(SettingsFilePath))
            {
                var json = File.ReadAllText(SettingsFilePath);
                return JsonSerializer.Deserialize<UserSettings>(json) ?? new UserSettings();
            }

            return new UserSettings();
        }

        public void Save()
        {
            var json = JsonSerializer.Serialize(this);
            File.WriteAllText(SettingsFilePath, json);
        }
    }
}