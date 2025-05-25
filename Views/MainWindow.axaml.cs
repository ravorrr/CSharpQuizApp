using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Threading;
using CSharpQuizApp.ViewModels;
using System;
using System.Threading.Tasks;
using System.Timers;

namespace CSharpQuizApp.Views;

public partial class MainWindow : Window
{
    private MainWindowViewModel _viewModel;
    private Timer _timer;
    private int _remainingSeconds = 15;
    private bool _answered;

    public MainWindow()
    {
        InitializeComponent();
        _viewModel = new MainWindowViewModel();
        UpdateUI();
    }

    private void StartTimer()
    {
        _remainingSeconds = 15;
        _answered = false;
        TimerTextBlock.Text = $"Pozostały czas: {_remainingSeconds} s";

        _timer = new Timer(1000);
        _timer.Elapsed += TimerElapsed!;
        _timer.Start();
    }

    private async void TimerElapsed(object? sender, ElapsedEventArgs e)
    {
        _remainingSeconds--;

        await Dispatcher.UIThread.InvokeAsync(() =>
        {
            TimerTextBlock.Text = $"Pozostały czas: {_remainingSeconds} s";
        });

        if (_remainingSeconds <= 0)
        {
            _timer?.Stop();
            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                if (!_answered)
                    ShowResult(-1);
            });
        }
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

        ResetButtonColors();
        StartTimer();
    }

    private void Answer1_Click(object? sender, RoutedEventArgs e) => ShowResult(0);
    private void Answer2_Click(object? sender, RoutedEventArgs e) => ShowResult(1);
    private void Answer3_Click(object? sender, RoutedEventArgs e) => ShowResult(2);
    private void Answer4_Click(object? sender, RoutedEventArgs e) => ShowResult(3);

    private async void ShowResult(int index)
    {
        if (_answered) return;
        _answered = true;
        _timer?.Stop();

        if (index == -1)
        {
            FeedbackTextBlock.Text = "Czas minął! Brak odpowiedzi.";
        }
        else
        {
            _viewModel.CheckAnswer(index);
            FeedbackTextBlock.Text = _viewModel.FeedbackMessage;
        }

        ScoreTextBlock.Text = $"Wynik: {_viewModel.Score}/{_viewModel.Questions.Count}";

        for (int i = 0; i < 4; i++)
        {
            var btn = GetButtonByIndex(i);

            if (index == -1)
            {
                btn.Background = (i == _viewModel.CurrentQuestion.CorrectAnswerIndex)
                    ? Brushes.LightGreen
                    : Brushes.DimGray;
            }
            else if (index == _viewModel.CurrentQuestion.CorrectAnswerIndex)
            {
                btn.Background = (i == index)
                    ? Brushes.LightGreen
                    : Brushes.DimGray;
            }
            else
            {
                if (i == index)
                    btn.Background = Brushes.IndianRed;
                else if (i == _viewModel.CurrentQuestion.CorrectAnswerIndex)
                    btn.Background = Brushes.LightGreen;
                else
                    btn.Background = Brushes.DimGray;
            }
        }

        await Task.Delay(4000);

        if (_viewModel.GoToNextQuestion())
        {
            _answered = false;
            ResetButtonColors();
            UpdateUI();
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

    private void ResetButtonColors()
    {
        foreach (var btn in new[] { Answer1Button, Answer2Button, Answer3Button, Answer4Button })
        {
            btn.Background = Brushes.DimGray;
            btn.ClearValue(Button.ForegroundProperty);
            btn.IsEnabled = true;
        }
    }

    private void RestartButton_Click(object? sender, RoutedEventArgs e)
    {
        _viewModel = new MainWindowViewModel();
        ResetButtonColors();

        QuestionTextBlock.IsVisible = true;
        FeedbackTextBlock.IsVisible = true;
        ScoreTextBlock.IsVisible = true;
        QuestionNumberTextBlock.IsVisible = true;
        TimerTextBlock.IsVisible = true;
        Answer1Button.IsVisible = true;
        Answer2Button.IsVisible = true;
        Answer3Button.IsVisible = true;
        Answer4Button.IsVisible = true;

        FinalResultTextBlock.IsVisible = false;
        RestartButton.IsVisible = false;

        UpdateUI();
    }

    private void ShowFinalScreen()
    {
        QuestionTextBlock.IsVisible = false;
        FeedbackTextBlock.IsVisible = false;
        ScoreTextBlock.IsVisible = false;
        QuestionNumberTextBlock.IsVisible = false;
        TimerTextBlock.IsVisible = false;
        Answer1Button.IsVisible = false;
        Answer2Button.IsVisible = false;
        Answer3Button.IsVisible = false;
        Answer4Button.IsVisible = false;

        FinalResultTextBlock.Text = $"Gratulacje! Twój wynik: {_viewModel.Score}/{_viewModel.Questions.Count}";
        FinalResultTextBlock.IsVisible = true;
        RestartButton.IsVisible = true;
    }
}
