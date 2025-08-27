using QuizApp.Data;

namespace QuizApp.ViewModels;

public class AllQuestionsQuizViewModel : QuizBaseViewModel
{
    public override string QuizTypeName => "Wszystkie";

    public AllQuestionsQuizViewModel()
    {
        QuizDatabase.Initialize();
        var questions = QuizDatabase.LoadAllQuestions();
        SetQuestions(questions);
        
        var settings = UserSettings.Load();
        PlayerName = string.IsNullOrWhiteSpace(settings.PlayerName) ? "Unknown" : settings.PlayerName;
    }
}