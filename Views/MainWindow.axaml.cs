using Avalonia.Controls;
using CSharpQuizApp.Data;
using CSharpQuizApp.ViewModels;

namespace CSharpQuizApp.Views;

public enum QuizType
{
    Random,
    All,
    Category
}

public partial class MainWindow : Window
{
    private string? _lastCategory;
    private QuizType _lastQuizType;

    public MainWindow()
    {
        InitializeComponent();
        MainContent.Content = new StartView(this);
    }
    
    private void SetPlayerName(QuizBaseViewModel viewModel)
    {
        var userSettings = UserSettings.Load();
        viewModel.PlayerName = string.IsNullOrWhiteSpace(userSettings.PlayerName) ? "Unknown" : userSettings.PlayerName;
    }

    public void StartRandomQuiz()
    {
        _lastQuizType = QuizType.Random;
        _lastCategory = null;
        var viewModel = new RandomQuizViewModel();
        SetPlayerName(viewModel);
        MainContent.Content = new QuizView(this, viewModel);
    }

    public void StartAllQuestionsQuiz()
    {
        _lastQuizType = QuizType.All;
        _lastCategory = null;
        var viewModel = new AllQuestionsQuizViewModel();
        SetPlayerName(viewModel);
        MainContent.Content = new QuizView(this, viewModel);
    }
    
    public void StartCategoryQuiz(string category)
    {
        _lastQuizType = QuizType.Category;
        _lastCategory = category;
        var viewModel = new CategoryQuizViewModel(category);
        SetPlayerName(viewModel);
        MainContent.Content = new QuizView(this, viewModel);
    }

    public void PlayAgain()
    {
        switch (_lastQuizType)
        {
            case QuizType.Random:
                StartRandomQuiz();
                break;
            case QuizType.All:
                StartAllQuestionsQuiz();
                break;
            case QuizType.Category:
                if (!string.IsNullOrEmpty(_lastCategory))
                    StartCategoryQuiz(_lastCategory);
                break;
        }
    }

    public void NavigateToStart()
    {
        MainContent.Content = new StartView(this);
    }

    public void NavigateToModeSelection()
    {
        MainContent.Content = new ModeSelectionView(this);
    }
    
    public void NavigateToCategorySelection()
    {
        MainContent.Content = new CategorySelectionView(this);
    }

    public void NavigateToHistory()
    {
        MainContent.Content = new HistoryView(this);
    }
    
    public void NavigateToStatistics()
    {
        MainContent.Content = new StatisticsView(this);
    }
    
    public void NavigateToRules()
    {
        MainContent.Content = new RulesView(this);
    }
}