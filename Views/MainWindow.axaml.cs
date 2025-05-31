using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CSharpQuizApp.Data;
using CSharpQuizApp.ViewModels;

namespace CSharpQuizApp.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        MainContent.Content = new StartView(this);
    }

    public void NavigateToQuiz(QuizBaseViewModel viewModel)
    {
        var userSettings = UserSettings.Load();
        viewModel.PlayerName = string.IsNullOrWhiteSpace(userSettings.PlayerName) ? "Unknown" : userSettings.PlayerName;

        MainContent.Content = new QuizView(this, viewModel);
    }

    public void NavigateToStart()
    {
        MainContent.Content = new StartView(this);
    }

    public void NavigateToModeSelection()
    {
        MainContent.Content = new ModeSelectionView(this);
    }

    public void NavigateToHistory()
    {
        MainContent.Content = new HistoryView(this);
    }
}