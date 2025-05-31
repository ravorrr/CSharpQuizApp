using System;
using CSharpQuizApp.Data;

namespace CSharpQuizApp.ViewModels;

public class RandomQuizViewModel : QuizBaseViewModel
{
    public override string QuizTypeName => "Losowy";

    public RandomQuizViewModel()
    {
        QuizDatabase.Initialize();
        var questions = QuizDatabase.LoadQuestions(); // 10 losowych
        SetQuestions(questions);
    }
}