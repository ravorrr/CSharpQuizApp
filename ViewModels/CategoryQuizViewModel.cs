using QuizApp.Data;

namespace QuizApp.ViewModels;

public class CategoryQuizViewModel : QuizBaseViewModel
{
    public string SelectedCategory { get; }

    public override string QuizTypeName => SelectedCategory;

    public CategoryQuizViewModel(string category)
    {
        SelectedCategory = category;
        var questions = QuizDatabase.LoadQuestionsByCategory(category);
        SetQuestions(questions);
    }
}