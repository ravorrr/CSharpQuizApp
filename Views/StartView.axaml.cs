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
        _mainWindow.NavigateToModeSelection();
    }

    private void ShowRules_Click(object? sender, RoutedEventArgs e)
    {
        RulesTextBlock.IsVisible = !RulesTextBlock.IsVisible;
    }
}