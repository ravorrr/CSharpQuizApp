using System;
using System.Linq;
using CSharpQuizApp.Data;
using CSharpQuizApp.Models;

namespace CSharpQuizApp.ViewModels;

public class SurvivalQuizViewModel : QuizBaseViewModel
{
    public override string QuizTypeName => "Survival";
    public bool IsAlive { get; private set; } = true;

    public SurvivalQuizViewModel()
    {
        QuizDatabase.Initialize();
        var qs = QuizDatabase.LoadAllQuestions();
        qs = qs.OrderBy(_ => Random.Shared.Next()).ToList();
        SetQuestions(qs);

        var settings = UserSettings.Load();
        PlayerName = string.IsNullOrWhiteSpace(settings.PlayerName) ? "Unknown" : settings.PlayerName;
    }

    public override void CheckAnswer(int index)
    {
        base.CheckAnswer(index);
        if (!IsAnswerCorrect) IsAlive = false;
    }

    public override bool GoToNextQuestion()
    {
        if (!IsAlive) return false;
        return base.GoToNextQuestion();
    }
}