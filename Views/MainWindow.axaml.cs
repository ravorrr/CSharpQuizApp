using Avalonia.Controls;
using Avalonia.Interactivity;
using CSharpQuizApp.ViewModels;
using System;
using System.Threading.Tasks;

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
        Answer1Button.Content = _viewModel.CurrentQuestion.Answers[0];
        Answer2Button.Content = _viewModel.CurrentQuestion.Answers[1];
        Answer3Button.Content = _viewModel.CurrentQuestion.Answers[2];
        Answer4Button.Content = _viewModel.CurrentQuestion.Answers[3];

        ScoreTextBlock.Text = $"Wynik: {_viewModel.Score}/{_viewModel.Questions.Count}";
        QuestionNumberTextBlock.Text = $"Pytanie {_viewModel.CurrentQuestionIndex + 1} z {_viewModel.Questions.Count}";
    }

    private void Answer1_Click(object? sender, RoutedEventArgs e) => ShowResult(0);
    private void Answer2_Click(object? sender, RoutedEventArgs e) => ShowResult(1);
    private void Answer3_Click(object? sender, RoutedEventArgs e) => ShowResult(2);
    private void Answer4_Click(object? sender, RoutedEventArgs e) => ShowResult(3);

    private async void ShowResult(int index)
    {
        _viewModel.CheckAnswer(index);
        FeedbackTextBlock.Text = _viewModel.FeedbackMessage;
        ScoreTextBlock.Text = $"Wynik: {_viewModel.Score}/{_viewModel.Questions.Count}";

        DisableAnswerButtons();

        await Task.Delay(2000);

        if (_viewModel.GoToNextQuestion())
        {
            UpdateUI();
            EnableAnswerButtons();
        }
        else
        {
            ShowFinalScreen();
        }
    }

    private Button GetButtonByIndex(int index)
    {
        return index switch
        {
            0 => Answer1Button,
            1 => Answer2Button,
            2 => Answer3Button,
            3 => Answer4Button,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private void DisableAnswerButtons()
    {
        Answer1Button.IsEnabled = false;
        Answer2Button.IsEnabled = false;
        Answer3Button.IsEnabled = false;
        Answer4Button.IsEnabled = false;
    }

    private void EnableAnswerButtons()
    {
        Answer1Button.IsEnabled = true;
        Answer2Button.IsEnabled = true;
        Answer3Button.IsEnabled = true;
        Answer4Button.IsEnabled = true;
    }

    private void NextButton_Click(object? sender, RoutedEventArgs e)
    {
        // Przycisk "Dalej" nieużywany - quiz przechodzi automatycznie
    }

    private void RestartButton_Click(object? sender, RoutedEventArgs e)
    {
        _viewModel = new MainWindowViewModel();
        UpdateUI();

        QuestionTextBlock.IsVisible = true;
        FeedbackTextBlock.IsVisible = true;
        ScoreTextBlock.IsVisible = true;
        QuestionNumberTextBlock.IsVisible = true;
        Answer1Button.IsVisible = true;
        Answer2Button.IsVisible = true;
        Answer3Button.IsVisible = true;
        Answer4Button.IsVisible = true;
        NextButton.IsVisible = true;

        FinalResultTextBlock.IsVisible = false;
        RestartButton.IsVisible = false;

        EnableAnswerButtons();
    }

    private void ShowFinalScreen()
    {
        QuestionTextBlock.IsVisible = false;
        FeedbackTextBlock.IsVisible = false;
        ScoreTextBlock.IsVisible = false;
        QuestionNumberTextBlock.IsVisible = false;
        Answer1Button.IsVisible = false;
        Answer2Button.IsVisible = false;
        Answer3Button.IsVisible = false;
        Answer4Button.IsVisible = false;
        NextButton.IsVisible = false;

        FinalResultTextBlock.Text = $"Gratulacje! Twój wynik: {_viewModel.Score}/{_viewModel.Questions.Count}";
        FinalResultTextBlock.IsVisible = true;
        RestartButton.IsVisible = true;
    }
}
