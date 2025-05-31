using System.Collections.Generic;
using CSharpQuizApp.Models;
using CSharpQuizApp.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace CSharpQuizApp.ViewModels;

public abstract class QuizBaseViewModel : ObservableObject
{
    public List<Question> Questions { get; protected set; }
    public Question CurrentQuestion { get; protected set; }
    public int Score { get; protected set; }
    public int CurrentQuestionIndex { get; protected set; }
    public bool IsAnswerCorrect { get; protected set; }
    public string FeedbackMessage { get; protected set; }
    
    public string PlayerName { get; set; } = "";
    public virtual string QuizTypeName => "Nieznany";
    public int QuizTimeSeconds { get; set; } = 0;
    public int TotalQuestions => Questions?.Count ?? 0;
    public int CorrectAnswers => Score;
    public int WrongAnswers => TotalQuestions - Score;

    protected QuizBaseViewModel()
    {
        CurrentQuestionIndex = 0;
        Score = 0;
        FeedbackMessage = "";
    }

    protected void SetQuestions(List<Question> questions)
    {
        Questions = questions;
        CurrentQuestion = Questions[CurrentQuestionIndex];
    }

    public virtual void CheckAnswer(int selectedIndex)
    {
        IsAnswerCorrect = CurrentQuestion.IsCorrect(selectedIndex);
        FeedbackMessage = IsAnswerCorrect ? "Poprawna odpowiedź!" : "Zła odpowiedź!";
        if (IsAnswerCorrect)
            Score++;
    }

    public bool GoToNextQuestion()
    {
        CurrentQuestionIndex++;

        if (CurrentQuestionIndex < Questions.Count)
        {
            CurrentQuestion = Questions[CurrentQuestionIndex];
            FeedbackMessage = "";
            return true;
        }

        return false;
    }
    
    public void SaveQuizHistory()
    {
        var playerName = string.IsNullOrWhiteSpace(PlayerName) ? "Unknown" : PlayerName;

        var entry = new QuizHistoryEntry
        {
            PlayerName = playerName,
            QuizType = QuizTypeName,
            Score = Score,
            TotalQuestions = TotalQuestions,
            CorrectAnswers = CorrectAnswers,
            WrongAnswers = WrongAnswers,
            TimeInSeconds = QuizTimeSeconds,
            Date = DateTime.Now
        };

        QuizDatabase.SaveQuizHistory(entry);
    }
}