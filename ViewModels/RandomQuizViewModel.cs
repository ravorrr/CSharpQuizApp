using CSharpQuizApp.Data;

namespace CSharpQuizApp.ViewModels;

public class RandomQuizViewModel : QuizBaseViewModel
{
    public override string QuizTypeName => "Losowy";

    public RandomQuizViewModel()
    {
        QuizDatabase.Initialize();
        var questions = QuizDatabase.LoadQuestions();
        SetQuestions(questions);
        
        var settings = UserSettings.Load();
        PlayerName = string.IsNullOrWhiteSpace(settings.PlayerName) ? "Unknown" : settings.PlayerName;
    }
}