using System;

namespace CSharpQuizApp.Models;

public class QuizHistoryEntry
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
    public string TimeText => $"{TimeInSeconds}s";
    public string DateText => Date.ToString("dd.MM.yyyy HH:mm:ss");
}