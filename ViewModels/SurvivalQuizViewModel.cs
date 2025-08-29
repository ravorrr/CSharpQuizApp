using System;
using System.Linq;
using QuizApp.Data;

namespace QuizApp.ViewModels
{
    public class SurvivalQuizViewModel : QuizBaseViewModel
    {
        public override string ModeKey => "Mode_Survival";
        
        public override string QuizTypeName => "Survival";

        public bool IsAlive { get; private set; } = true;

        public SurvivalQuizViewModel()
        {
            InitNewRun();
        }

        private void InitNewRun()
        {
            QuizDatabase.Initialize();

            var qs = QuizDatabase.LoadAllQuestions()
                .OrderBy(_ => Random.Shared.Next())
                .ToList();

            SetQuestions(qs);
            Score = 0;
            CurrentQuestionIndex = 0;
            IsAlive = true;

            var settings = UserSettings.Load();
            PlayerName = string.IsNullOrWhiteSpace(settings.PlayerName) ? "Unknown" : settings.PlayerName;
        }
        
        public void ResetForReplay() => InitNewRun();

        public override void CheckAnswer(int index)
        {
            base.CheckAnswer(index);
            if (!IsAnswerCorrect)
                IsAlive = false;
        }

        public override bool GoToNextQuestion()
        {
            if (!IsAlive)
                return false;

            return base.GoToNextQuestion();
        }
    }
}