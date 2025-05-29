using System;
using System.Linq;
using CSharpQuizApp.Data;

namespace CSharpQuizApp.ViewModels;

public class RandomQuizViewModel : QuizBaseViewModel
{
    public RandomQuizViewModel()
    {
        QuizDatabase.Initialize();
        var questions = QuizDatabase.LoadQuestions(); // 10 losowych
        SetQuestions(questions);
    }
}