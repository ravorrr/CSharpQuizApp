using System;
using System.Linq;
using CSharpQuizApp.Data;
using CSharpQuizApp.Models;
using CSharpQuizApp.Localization;

namespace CSharpQuizApp.ViewModels;

public class SurvivalQuizViewModel : QuizBaseViewModel
{
    public override string ModeKey => "Mode_Survival";
    public override string QuizTypeName => LocalizationService.L[ModeKey];

    private bool _isDead;
    public bool IsAlive => !_isDead;

    public SurvivalQuizViewModel()
    {
        QuizDatabase.Initialize();

        var qs = QuizDatabase.LoadAllQuestions()
            .OrderBy(_ => Random.Shared.Next())
            .ToList();

        SetQuestions(qs);

        var settings = UserSettings.Load();
        PlayerName = string.IsNullOrWhiteSpace(settings.PlayerName) ? "Unknown" : settings.PlayerName;
    }

    public override void CheckAnswer(int index)
    {
        if (_isDead || CurrentQuestion is null)
            return;

        base.CheckAnswer(index);

        if (!IsAnswerCorrect)
        {
            _isDead = true;
        }
        else
        {
            
        }
    }

    public override bool GoToNextQuestion()
    {
        if (_isDead)
            return false;
        
        return base.GoToNextQuestion();
    }
}