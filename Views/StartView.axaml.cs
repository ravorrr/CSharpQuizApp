using Avalonia.Controls;
using Avalonia.Interactivity;

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
        _mainWindow.NavigateToQuiz();
    }

    private void ShowRules_Click(object? sender, RoutedEventArgs e)
    {
        RulesTextBlock.IsVisible = !RulesTextBlock.IsVisible;
    }
}