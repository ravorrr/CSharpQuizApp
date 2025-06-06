using Avalonia.Controls;
using Avalonia.Interactivity;
using CSharpQuizApp.Data;
using CSharpQuizApp.Models;
using CSharpQuizApp.ViewModels;

namespace CSharpQuizApp.Views;

public partial class StatisticsView : UserControl
{
    private readonly MainWindow _mainWindow;

    public StatisticsView(MainWindow mainWindow)
    {
        InitializeComponent();
        _mainWindow = mainWindow;
        
        var history = QuizDatabase.LoadHistory();
        var stats = new StatisticsViewModel(history);
        
        TotalQuizzesTextBlock.Text = $"Liczba rozegranych quizów: {stats.TotalQuizzes}";
        AverageScoreTextBlock.Text = $"Średni wynik: {stats.AverageScorePercent}%";
        BestScoreTextBlock.Text = $"Najlepszy wynik: {stats.BestScore}";
        WorstScoreTextBlock.Text = $"Najgorszy wynik: {stats.WorstScore}";
        AverageTimeTextBlock.Text = $"Średni czas quizu: {stats.AverageTimeSeconds} s";
    }

    private void BackToMenu_Click(object? sender, RoutedEventArgs e)
    {
        _mainWindow.NavigateToStart();
    }
}