using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Threading;
using CSharpQuizApp.ViewModels;
using System;
using System.Threading.Tasks;
using System.Timers;

namespace CSharpQuizApp.Views;

public partial class QuizView : UserControl
{
    private MainWindow _mainWindow;
    private QuizBaseViewModel _viewModel;
    private Timer? _timer;
    private int _remainingSeconds = 15;
    private bool _answered;
    private DateTime _quizStartTime;

    public QuizView(MainWindow mainWindow, QuizBaseViewModel viewModel)
    {
        InitializeComponent();
        _mainWindow = mainWindow;
        _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
        UpdateUi();
        _quizStartTime = DateTime.Now;
    }

    private void StartTimer()
    {
        _remainingSeconds = 15;
        _answered = false;
        TimerTextBlock.Text = $"Pozostały czas: {_remainingSeconds} s";

        _timer = new Timer(1000);
        _timer.Elapsed += TimerElapsed;
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

    private void UpdateUi()
    {
        if (_viewModel.Questions.Count == 0)
        {
            FeedbackTextBlock.Text = "❌ Błąd ładowania pytań. Spróbuj ponownie.";
            FeedbackTextBlock.Foreground = Brushes.OrangeRed;

            QuestionTextBlock.IsVisible = false;
            QuestionNumberTextBlock.IsVisible = false;
            TimerTextBlock.IsVisible = false;

            DisableAllAnswerButtons();
            Answer1Button.IsVisible = false;
            Answer2Button.IsVisible = false;
            Answer3Button.IsVisible = false;
            Answer4Button.IsVisible = false;

            BackToMenuButton.IsVisible = true;
            return;
        }

        QuestionTextBlock.Text = _viewModel.CurrentQuestion!.Text;

        Answer1Button.Content = _viewModel.CurrentQuestion!.Answers[0];
        Answer2Button.Content = _viewModel.CurrentQuestion!.Answers[1];
        Answer3Button.Content = _viewModel.CurrentQuestion!.Answers[2];
        Answer4Button.Content = _viewModel.CurrentQuestion!.Answers[3];

        ScoreTextBlock.Text = $"Wynik: {_viewModel.Score}/{_viewModel.Questions.Count}";
        QuestionNumberTextBlock.Text = $"Pytanie {_viewModel.CurrentQuestionIndex + 1} z {_viewModel.Questions.Count}";

        FeedbackTextBlock.Text = "";
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

        DisableAllAnswerButtons();

        if (index == -1)
        {
            FeedbackTextBlock.Text = "Czas minął! Brak odpowiedzi.";
            FeedbackTextBlock.Foreground = Brushes.Orange;
        }
        else
        {
            _viewModel.CheckAnswer(index);

            if (_viewModel.IsAnswerCorrect)
            {
                FeedbackTextBlock.Text = "Poprawna odpowiedź!";
                FeedbackTextBlock.Foreground = Brushes.LightGreen;
            }
            else
            {
                FeedbackTextBlock.Text = "Zła odpowiedź!";
                FeedbackTextBlock.Foreground = Brushes.IndianRed;
            }
        }

        ScoreTextBlock.Text = $"Wynik: {_viewModel.Score}/{_viewModel.Questions.Count}";
        
        ColorAnswerButtons(index);

        await Task.Delay(3000);

        if (_viewModel.GoToNextQuestion())
        {
            _answered = false;
            ResetButtonColors();
            UpdateUi();
        }
        else
        {
            ShowFinalScreen();
        }
    }
    
    private void ColorAnswerButtons(int index)
    {
        for (int i = 0; i < 4; i++)
        {
            var btn = GetButtonByIndex(i);

            if (index == -1)
            {
                btn.Background = (i == _viewModel.CurrentQuestion!.CorrectAnswerIndex)
                    ? Brushes.LightGreen
                    : Brushes.DimGray;
            }
            else if (index == _viewModel.CurrentQuestion!.CorrectAnswerIndex)
            {
                btn.Background = (i == index)
                    ? Brushes.LightGreen
                    : Brushes.DimGray;
            }
            else
            {
                if (i == index)
                    btn.Background = Brushes.IndianRed;
                else if (i == _viewModel.CurrentQuestion!.CorrectAnswerIndex)
                    btn.Background = Brushes.LightGreen;
                else
                    btn.Background = Brushes.DimGray;
            }
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
            btn.ClearValue(Button.BackgroundProperty);
            btn.ClearValue(Button.ForegroundProperty);
            btn.IsEnabled = true;
        }
    }

    private void DisableAllAnswerButtons()
    {
        foreach (var btn in new[] { Answer1Button, Answer2Button, Answer3Button, Answer4Button })
        {
            btn.IsEnabled = false;
        }
    }

    private void RestartButton_Click(object? sender, RoutedEventArgs e)
    {
        _mainWindow.PlayAgain();
    }

    private void ShowFinalScreen()
    {
        _viewModel.QuizTimeSeconds = (int)(DateTime.Now - _quizStartTime).TotalSeconds;
        _viewModel.SaveQuizHistory();
        
        QuestionTextBlock.IsVisible = false;
        FeedbackTextBlock.IsVisible = false;
        ScoreTextBlock.IsVisible = false;
        QuestionNumberTextBlock.IsVisible = false;
        TimerTextBlock.IsVisible = false;
        Answer1Button.IsVisible = false;
        Answer2Button.IsVisible = false;
        Answer3Button.IsVisible = false;
        Answer4Button.IsVisible = false;
        ExitButton.IsVisible = false;

        FinalResultTextBlock.Text = $"Gratulacje! Twój wynik: {_viewModel.Score}/{_viewModel.Questions.Count}";
        FinalResultTextBlock.IsVisible = true;
        RestartButton.IsVisible = true;
        BackToMenuButton.IsVisible = true;
    }
    
    private void BackToMenu_Click(object? sender, RoutedEventArgs e)
    {
        _mainWindow.NavigateToStart();
    }
    
    private void ExitButton_Click(object? sender, RoutedEventArgs e)
    {
        ConfirmExitOverlay.IsVisible = true;
    }
    
    private void ConfirmExit_Yes_Click(object? sender, RoutedEventArgs e)
    {
        _mainWindow.NavigateToStart();
    }

    private void ConfirmExit_No_Click(object? sender, RoutedEventArgs e)
    {
        ConfirmExitOverlay.IsVisible = false;
    }
}
