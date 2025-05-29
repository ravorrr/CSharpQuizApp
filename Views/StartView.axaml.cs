using Avalonia.Controls;
using Avalonia.Interactivity;
using CSharpQuizApp.ViewModels;

namespace CSharpQuizApp.Views;

public partial class StartView : UserControl
{
    private readonly MainWindow _mainWindow;

    public StartView(MainWindow mainWindow)
    {
        InitializeComponent();
        _mainWindow = mainWindow;
    }

    private void StartQuiz_Click(object? sender, RoutedEventArgs e)
    {
        var quizVM = new RandomQuizViewModel();
        _mainWindow.MainContent.Content = new QuizView(_mainWindow, quizVM);
    }

    private void ShowRules_Click(object? sender, RoutedEventArgs e)
    {
        RulesTextBlock.IsVisible = !RulesTextBlock.IsVisible;
    }
}