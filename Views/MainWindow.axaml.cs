using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CSharpQuizApp.ViewModels;

namespace CSharpQuizApp.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        MainContent.Content = new StartView(this); // Start from menu
    }

    public void NavigateToQuiz(QuizBaseViewModel viewModel)
    {
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
}