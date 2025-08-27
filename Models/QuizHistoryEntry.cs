using System;
using System.ComponentModel;
using CSharpQuizApp.Localization;

namespace CSharpQuizApp.Models
{
    public class QuizHistoryEntry : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string PlayerName { get; set; } = "";
        public string QuizType { get; set; } = "";
        public int Score { get; set; }
        public int TotalQuestions { get; set; }
        public int CorrectAnswers { get; set; }
        public int WrongAnswers { get; set; }
        public int TimeInSeconds { get; set; }
        public DateTime Date { get; set; }

        public string ScoreText => $"{Score}/{TotalQuestions}";
        public string TimeText  => $"{TimeInSeconds}s";
        public string DateText  => Date.ToString("dd.MM.yyyy HH:mm:ss");
        
        public string QuizTypeLocalized => MapMode(QuizType);

        public event PropertyChangedEventHandler? PropertyChanged;

        public QuizHistoryEntry()
        {
            LocalizationService.Instance.Localizer.PropertyChanged += Localizer_PropertyChanged;
        }

        private void Localizer_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.PropertyName) || e.PropertyName == "Item[]")
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(QuizTypeLocalized)));
        }

        private static string MapMode(string raw)
        {
            var v = (raw ?? string.Empty).Trim();
            
            if (v.StartsWith("Mode_", StringComparison.Ordinal))
                return LocalizationService.L[v];
            
            v = v.ToLowerInvariant();

            string key = v switch
            {
                "losowy" or "quiz losowy"                             => "Mode_Random",
                "pełny" or "pelny" or "quiz pełny" or "quiz pelny"    => "Mode_All",
                "kategoria" or "wg kategorii" or "quiz wg kategorii"  => "Mode_ByCategory",

                "random" or "random quiz"     => "Mode_Random",
                "all" or "full" or "full quiz"=> "Mode_All",
                "category" or "category quiz" => "Mode_ByCategory",

                _ => "Mode_Random"
            };

            return LocalizationService.L[key];
        }
    }
}
