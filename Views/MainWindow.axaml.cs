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

    public void NavigateToQuiz()
    {
        var quizVM = new RandomQuizViewModel();
        MainContent.Content = new QuizView(this, quizVM);
    }
    
    public void NavigateToStart()
    {
        MainContent.Content = new StartView(this);
    }
}