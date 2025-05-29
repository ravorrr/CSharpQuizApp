using System.Collections.Generic;
using CSharpQuizApp.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CSharpQuizApp.ViewModels;

public abstract class QuizBaseViewModel : ObservableObject
{
    public List<Question> Questions { get; protected set; }
    public Question CurrentQuestion { get; protected set; }
    public int Score { get; protected set; }
    public int CurrentQuestionIndex { get; protected set; }
    public bool IsAnswerCorrect { get; protected set; }
    public string FeedbackMessage { get; protected set; }

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
}