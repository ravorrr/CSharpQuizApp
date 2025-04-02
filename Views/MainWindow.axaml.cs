using Avalonia.Controls;
using Avalonia.Interactivity;
using CSharpQuizApp.ViewModels;

namespace CSharpQuizApp.Views;

public partial class MainWindow : Window
{
    private MainWindowViewModel _viewModel;

    public MainWindow()
    {
        InitializeComponent();
        
        _viewModel = new MainWindowViewModel();
        
        QuestionTextBlock.Text = _viewModel.CurrentQuestion.Text;

        Answer1Button.Content = _viewModel.CurrentQuestion.Answers[0];
        Answer2Button.Content = _viewModel.CurrentQuestion.Answers[1];
        Answer3Button.Content = _viewModel.CurrentQuestion.Answers[2];
        Answer4Button.Content = _viewModel.CurrentQuestion.Answers[3];
    }

    private void Answer1_Click(object? sender, RoutedEventArgs e) => ShowResult(0);
    private void Answer2_Click(object? sender, RoutedEventArgs e) => ShowResult(1);
    private void Answer3_Click(object? sender, RoutedEventArgs e) => ShowResult(2);
    private void Answer4_Click(object? sender, RoutedEventArgs e) => ShowResult(3);

    private void ShowResult(int index)
    {
        _viewModel.CheckAnswer(index);
        FeedbackTextBlock.Text = _viewModel.FeedbackMessage;
    }
}