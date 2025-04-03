using System.Threading.Tasks;
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
        UpdateUI();
    }

    private void UpdateUI()
    {
        QuestionTextBlock.Text = _viewModel.CurrentQuestion.Text;
        
        var answers = _viewModel.CurrentQuestion.Answers;
        Answer1Button.Content = answers[0];
        Answer2Button.Content = answers[1];
        Answer3Button.Content = answers[2];
        Answer4Button.Content = answers[3];

        FeedbackTextBlock.Text = _viewModel.FeedbackMessage;
        ScoreTextBlock.Text = $"Result: {_viewModel.Score}/{_viewModel.Questions.Count}";
        QuestionNumberTextBlock.Text = $"Question {_viewModel.CurrentQuestionIndex + 1} z {_viewModel.Questions.Count}";
    }

    private async void ShowResult(int index)
    {
        Answer1Button.IsEnabled = false;
        Answer2Button.IsEnabled = false;
        Answer3Button.IsEnabled = false;
        Answer4Button.IsEnabled = false;
        
        _viewModel.CheckAnswer(index);
        FeedbackTextBlock.Text = _viewModel.FeedbackMessage;
        ScoreTextBlock.Text = $"Wynik: {_viewModel.Score}/{_viewModel.Questions.Count}";
        
        await Task.Delay(2000);
        
        if (_viewModel.GoToNextQuestion())
        {
            UpdateUI();
            Answer1Button.IsEnabled = true;
            Answer2Button.IsEnabled = true;
            Answer3Button.IsEnabled = true;
            Answer4Button.IsEnabled = true;
        }
        else
        {
            QuestionTextBlock.Text = "You finished quiz!";
            FeedbackTextBlock.Text = $"Your final score: {_viewModel.Score}/{_viewModel.Questions.Count}";

            Answer1Button.IsVisible = false;
            Answer2Button.IsVisible = false;
            Answer3Button.IsVisible = false;
            Answer4Button.IsVisible = false;
            NextButton.IsVisible = false;
            QuestionNumberTextBlock.IsVisible = false;
        }
    }

    private void NextButton_Click(object? sender, RoutedEventArgs e)
    {
        if (_viewModel.GoToNextQuestion())
        {
            UpdateUI();
        }
        else
        {
            QuestionTextBlock.Text = "You finished quiz!";
            FeedbackTextBlock.Text = $"Your final score: {_viewModel.Score}/{_viewModel.Questions.Count}";
            
            Answer1Button.IsVisible = false;
            Answer2Button.IsVisible = false;
            Answer3Button.IsVisible = false;
            Answer4Button.IsVisible = false;
            NextButton.IsVisible = false;
            QuestionNumberTextBlock.IsVisible = false;
        }
    }
    
    private void Answer1_Click(object? sender, RoutedEventArgs e) => ShowResult(0);
    private void Answer2_Click(object? sender, RoutedEventArgs e) => ShowResult(1);
    private void Answer3_Click(object? sender, RoutedEventArgs e) => ShowResult(2);
    private void Answer4_Click(object? sender, RoutedEventArgs e) => ShowResult(3);

}