using Avalonia.Controls;
using Avalonia.Interactivity;
using CSharpQuizApp.Data;
using CSharpQuizApp.Models;
using CSharpQuizApp.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace CSharpQuizApp.Views;

public partial class StatisticsView : UserControl
{
    private readonly MainWindow _mainWindow;
    private readonly List<QuizHistoryEntry> _allHistory;

    public StatisticsView(MainWindow mainWindow)
    {
        InitializeComponent();
        _mainWindow = mainWindow;

        _allHistory = QuizDatabase.LoadHistory();
        
        var playerNames = _allHistory
            .Select(h => string.IsNullOrWhiteSpace(h.PlayerName) ? "(Nieznany)" : h.PlayerName)
            .Distinct()
            .OrderBy(name => name)
            .ToList();
        
        playerNames.Insert(0, "Wszyscy gracze");

        PlayerComboBox.ItemsSource = playerNames;
        PlayerComboBox.SelectedIndex = 0;
        
        UpdateStats(_allHistory);
    }

    private void PlayerComboBox_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (PlayerComboBox.SelectedItem is string selected)
        {
            List<QuizHistoryEntry> filtered;
            if (selected == "Wszyscy gracze")
            {
                filtered = _allHistory;
            }
            else
            {
                filtered = _allHistory.Where(h => 
                    (!string.IsNullOrWhiteSpace(h.PlayerName) ? h.PlayerName : "(Nieznany)") == selected
                ).ToList();
            }
            UpdateStats(filtered);
        }
    }

    private void UpdateStats(List<QuizHistoryEntry> history)
    {
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