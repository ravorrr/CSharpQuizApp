using System;
using System.IO;
using System.Text.Json;

namespace CSharpQuizApp.Data
{
    public class UserSettings
    {
        public string PlayerName { get; set; } = "Unknown";

        private static readonly string FilePath = "usersettings.json";

        public static UserSettings Load()
        {
            try
            {
                if (File.Exists(FilePath))
                {
                    string json = File.ReadAllText(FilePath);
                    return JsonSerializer.Deserialize<UserSettings>(json) ?? new UserSettings();
                }
            }
            catch (Exception)
            {
                // Możesz dodać logowanie błędów
            }

            return new UserSettings();
        }

        public void Save()
        {
            try
            {
                string json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(FilePath, json);
            }
            catch (Exception)
            {
                // Możesz dodać logowanie błędów
            }
        }
    }
}