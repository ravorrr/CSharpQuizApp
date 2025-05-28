using Avalonia.Controls;
using Avalonia.Markup.Xaml;

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
        MainContent.Content = new QuizView(this);
    }
    
    public void NavigateToStart()
    {
        MainContent.Content = new StartView(this);
    }
}