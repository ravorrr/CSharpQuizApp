using System.Collections.Generic;
using System.IO;
using CSharpQuizApp.Data;
using CSharpQuizApp.Models;

namespace CSharpQuizApp.ViewModels;

public class MainWindowViewModel
{
    public List<Question> Questions { get; set; }
    public Question CurrentQuestion { get; set; }
    public string FeedbackMessage { get; set; }
    public int Score { get; set; }
    public int CurrentQuestionIndex { get; set; }

    public MainWindowViewModel()
    {
        QuizDatabase.Initialize();
        Questions = QuizDatabase.LoadQuestions();
        CurrentQuestion = Questions[0];
        FeedbackMessage = "";
        Score = 0;
        CurrentQuestion = Questions[CurrentQuestionIndex];
    }

    public void CheckAnswer(int selectedIndex)
    {
        if (CurrentQuestion.IsCorrect(selectedIndex))
        {
            FeedbackMessage = "✅ Correct Answer!";
            Score++;
        }
        else
        {
            FeedbackMessage = "❌ Wrong Answer!";
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