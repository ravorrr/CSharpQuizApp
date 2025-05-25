using System.Collections.Generic;
using System.IO;
using CSharpQuizApp.Data;
using CSharpQuizApp.Models;
using System;
using System.Linq;

namespace CSharpQuizApp.ViewModels;

public class MainWindowViewModel
{
    public List<Question> Questions { get; set; }
    public Question CurrentQuestion { get; set; }
    public string FeedbackMessage { get; set; }
    public int Score { get; set; }
    public int CurrentQuestionIndex { get; set; }
    public bool IsAnswerCorrect { get; private set; }

    public MainWindowViewModel()
    {
        QuizDatabase.Initialize();

        Questions = QuizDatabase.LoadQuestions()
            .OrderBy(q => Guid.NewGuid())
            .Take(10)
            .ToList();

        CurrentQuestionIndex = 0;
        CurrentQuestion = Questions[CurrentQuestionIndex];
        FeedbackMessage = "";
        Score = 0;
    }

    public void CheckAnswer(int selectedIndex)
    {
        IsAnswerCorrect = CurrentQuestion.IsCorrect(selectedIndex); // 🆕

        if (IsAnswerCorrect)
        {
            FeedbackMessage = "Poprawna odpowiedź!";
            Score++;
        }
        else
        {
            FeedbackMessage = "Zła odpowiedź!";
        }
        
        File.WriteAllText("result.txt", $"Your score is {Score}/{Questions.Count}");
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
}