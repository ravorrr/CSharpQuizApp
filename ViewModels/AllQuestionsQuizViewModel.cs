using CSharpQuizApp.Data;
using CSharpQuizApp.Models;
using System.Collections.Generic;

namespace CSharpQuizApp.ViewModels;

public class AllQuestionsQuizViewModel : QuizBaseViewModel
{
    public override string QuizTypeName => "Wszystkie";

    public AllQuestionsQuizViewModel()
    {
        QuizDatabase.Initialize();
        var questions = QuizDatabase.LoadAllQuestions();
        SetQuestions(questions);
    }
}