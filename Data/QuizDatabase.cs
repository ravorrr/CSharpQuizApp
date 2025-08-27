using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.Json;
using CSharpQuizApp.Models;
using CSharpQuizApp.Utils;
using Microsoft.Data.Sqlite;

namespace CSharpQuizApp.Data;

public static class QuizDatabase
{
    private const string QuestionsFolder = "Assets/Questions";

    private static readonly string[] PlCandidates = { "questions.pl.json", "questions_pl.json" };
    private static readonly string[] EnCandidates = { "questions.en.json", "questions_en.json" };

    private static readonly JsonSerializerOptions JsonOpts = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private sealed class JsonQuestion
    {
        public string Question { get; set; } = "";
        public List<string> Answers { get; set; } = new();
        public int CorrectIndex { get; set; }
        public string Category { get; set; } = "";
    }

    private const string DbFileName = "quiz.db";

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

    private static readonly string AppDataDir = GetAppDataDir();
    private static readonly string FilePath = Path.Combine(AppDataDir, DbFileName);

    private static string LegacyWindowsPath =>
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CSharpQuizApp", DbFileName);

    private static string LegacyMacDocumentsPath
    {
        get
        {
            var personal = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(personal, "Library", "Application Support", "Quiz App", DbFileName);
        }
    }

    public static string ConnectionString => $"Data Source={FilePath}";

    public static void Initialize()
    {
        try
        {
            Directory.CreateDirectory(AppDataDir);

            if (File.Exists(LegacyWindowsPath) && !File.Exists(FilePath))
            {
                try
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(FilePath) ?? AppDataDir);
                    File.Copy(LegacyWindowsPath, FilePath, overwrite: false);
#if DEBUG
                    Console.WriteLine(FormattableString.Invariant($"[DEBUG] Migrated quiz.db (Windows): {LegacyWindowsPath} → {FilePath}"));
#endif
                }
                catch (Exception ex) { Logger.LogError(ex); }
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                if (File.Exists(LegacyMacDocumentsPath) && !File.Exists(FilePath))
                {
                    try
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(FilePath) ?? AppDataDir);
                        File.Copy(LegacyMacDocumentsPath, FilePath, overwrite: false);
#if DEBUG
                        Console.WriteLine(FormattableString.Invariant($"[DEBUG] Migrated quiz.db (macOS Documents): {LegacyMacDocumentsPath} → {FilePath}"));
#endif
                    }
                    catch (Exception ex) { Logger.LogError(ex); }
                }
            }

            EnsureHistoryTableExists();
        }
        catch (Exception ex) { Logger.LogError(ex); }
    }

    private static void EnsureHistoryTableExists()
    {
        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();

        var cmd = connection.CreateCommand();
        cmd.CommandText =
            @"CREATE TABLE IF NOT EXISTS quiz_history (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                player_name TEXT,
                quiz_type TEXT,
                score INTEGER,
                total_questions INTEGER,
                correct_answers INTEGER,
                wrong_answers INTEGER,
                time_in_seconds INTEGER,
                date TEXT
            );";
        cmd.ExecuteNonQuery();
    }

    public static void SaveQuizHistory(QuizHistoryEntry entry)
    {
        try
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            using var cmd = connection.CreateCommand();
            cmd.CommandText =
                @"INSERT INTO quiz_history
                  (player_name, quiz_type, score, total_questions, correct_answers, wrong_answers, time_in_seconds, date)
                  VALUES (@name, @type, @score, @total, @correct, @wrong, @time, @date)";
            cmd.Parameters.AddWithValue("@name",   entry.PlayerName);
            cmd.Parameters.AddWithValue("@type",   entry.QuizType);
            cmd.Parameters.AddWithValue("@score",  entry.Score);
            cmd.Parameters.AddWithValue("@total",  entry.TotalQuestions);
            cmd.Parameters.AddWithValue("@correct",entry.CorrectAnswers);
            cmd.Parameters.AddWithValue("@wrong",  entry.WrongAnswers);
            cmd.Parameters.AddWithValue("@time",   entry.TimeInSeconds);
            cmd.Parameters.AddWithValue("@date",   entry.Date.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture));
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex) { Logger.LogError(ex); }
    }

    public static List<QuizHistoryEntry> LoadHistory()
    {
        var history = new List<QuizHistoryEntry>();
        try
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM quiz_history ORDER BY date DESC";

            using var r = cmd.ExecuteReader();
            while (r.Read())
            {
                history.Add(new QuizHistoryEntry
                {
                    Id              = r.GetInt32(0),
                    PlayerName      = r.GetString(1),
                    QuizType        = r.GetString(2),
                    Score           = r.GetInt32(3),
                    TotalQuestions  = r.GetInt32(4),
                    CorrectAnswers  = r.GetInt32(5),
                    WrongAnswers    = r.GetInt32(6),
                    TimeInSeconds   = r.GetInt32(7),
                    Date            = DateTime.Parse(r.GetString(8), CultureInfo.InvariantCulture)
                });
            }
        }
        catch (Exception ex) { Logger.LogError(ex); }

        return history;
    }

    public static void ClearHistory()
    {
        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();
        var cmd = connection.CreateCommand();
        cmd.CommandText = "DELETE FROM quiz_history";
        cmd.ExecuteNonQuery();
    }

    public static List<Question> LoadQuestions(int limit = 10)
    {
        try
        {
            var list = ReadAllFromCurrentLanguage();
            return TakeRandom(ToQuestions(list), limit);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex);
            return new List<Question>();
        }
    }

    public static List<Question> LoadAllQuestions()
    {
        try
        {
            var list = ReadAllFromCurrentLanguage();
            return ToQuestions(list);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex);
            return new List<Question>();
        }
    }

    public static List<Question> LoadQuestionsByCategory(string categoryLabel, int limit = 10)
    {
        try
        {
            var wanted = NormalizeCategory(categoryLabel);
            var list = ReadAllFromCurrentLanguage()
                .Where(q => NormalizeCategory(q.Category) == wanted)
                .ToList();

            return TakeRandom(ToQuestions(list), limit);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex);
            return new List<Question>();
        }
    }

    private static List<JsonQuestion> ReadAllFromCurrentLanguage()
    {
        var lang = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName.ToLowerInvariant();
        var candidates = lang.StartsWith("pl") ? PlCandidates : EnCandidates;

        string BuildPath(string file) =>
            Path.Combine(AppContext.BaseDirectory,
                         QuestionsFolder.Replace('/', Path.DirectorySeparatorChar),
                         file);

        string? chosen = candidates.Select(BuildPath).FirstOrDefault(File.Exists);

        if (chosen is null)
        {
            var fb = (candidates == PlCandidates) ? EnCandidates : PlCandidates;
            chosen = fb.Select(BuildPath).FirstOrDefault(File.Exists);
        }

        if (chosen is null)
        {
            var diag = string.Join(", ",
                candidates.Select(BuildPath).Concat(
                    ((candidates == PlCandidates) ? EnCandidates : PlCandidates).Select(BuildPath)));
            throw new FileNotFoundException($"Nie znaleziono plików z pytaniami. Sprawdź: {diag}");
        }

        var json = File.ReadAllText(chosen);
        var data = JsonSerializer.Deserialize<List<JsonQuestion>>(json, JsonOpts) ?? new();
        return data;
    }

    private static List<Question> ToQuestions(List<JsonQuestion> json) =>
        json.Select(j => new Question(j.Question, j.Answers, j.CorrectIndex)).ToList();

    private static List<Question> TakeRandom(List<Question> src, int limit) =>
        src.OrderBy(_ => Random.Shared.Next()).Take(limit).ToList();

    private static readonly Dictionary<string, string> CategoryMap = new(StringComparer.OrdinalIgnoreCase)
    {
        ["Geografia"] = "geography", ["Geography"] = "geography",
        ["Informatyka"] = "it", ["Computer science"] = "it", ["Computer Science"] = "it", ["IT"] = "it",
        ["Technologia"] = "technology", ["Technology"] = "technology",
        ["Astronomia"] = "astronomy", ["Astronomy"] = "astronomy",
        ["Historia"] = "history", ["History"] = "history",
        ["Chemia"] = "chemistry", ["Chemistry"] = "chemistry",
        ["Matematyka"] = "math", ["Mathematics"] = "math", ["Math"] = "math",
        ["Biologia"] = "biology", ["Biology"] = "biology",
        ["Fizyka"] = "physics", ["Physics"] = "physics",
        ["Literatura"] = "literature", ["Literature"] = "literature",
        ["Języki"] = "languages", ["Jezyki"] = "languages", ["Languages"] = "languages",
        ["Kultura"] = "culture", ["Culture"] = "culture",
        ["Muzyka"] = "music", ["Music"] = "music",
        ["Motoryzacja"] = "automotive", ["Automotive"] = "automotive", ["Cars"] = "automotive",
        ["Sport"] = "sport", ["Sports"] = "sport",
    };

    private static string NormalizeCategory(string value) =>
        CategoryMap.TryGetValue(value.Trim(), out var slug)
            ? slug
            : value.Trim().ToLowerInvariant();
}
