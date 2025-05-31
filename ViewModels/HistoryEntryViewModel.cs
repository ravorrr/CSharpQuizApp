using System;
using CSharpQuizApp.Models;

namespace CSharpQuizApp.ViewModels;

public class HistoryEntryViewModel
{
    private readonly QuizHistoryEntry _entry;

    public HistoryEntryViewModel(QuizHistoryEntry entry)
    {
        _entry = entry;
    }

    public string PlayerName => _entry.PlayerName;
    public string QuizType => _entry.QuizType;
    public string ScoreText => $"Wynik: {_entry.CorrectAnswers}/{_entry.TotalQuestions}";
    public string TimeText => $"Czas: {_entry.TimeInSeconds} sek.";
    public string DateText => _entry.Date.ToString("yyyy-MM-dd HH:mm");

    public string FormattedEntry =>
        $"{DateText} | {PlayerName} | {QuizType} | {ScoreText} | {TimeText}";
}