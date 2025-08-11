using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using CSharpQuizApp.Data;
using CSharpQuizApp.Localization;
using CSharpQuizApp.Models;

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
        
        BuildPlayersListAndBind();
        UpdateForSelection();
        
        LocalizationService.Instance.Localizer.PropertyChanged += (_, e) =>
        {
            if (string.IsNullOrEmpty(e.PropertyName) || e.PropertyName == "Item[]")
            {
                var keepIndex = PlayerComboBox.SelectedIndex;
                BuildPlayersListAndBind(keepIndex);
                UpdateForSelection();
            }
        };
    }

    private void PlayerComboBox_SelectionChanged(object? sender, SelectionChangedEventArgs e)
        => UpdateForSelection();

    private void BackToMenu_Click(object? sender, RoutedEventArgs e)
        => _mainWindow.NavigateToStart();
    

    private static bool IsUnknownName(string? name)
        => string.IsNullOrWhiteSpace(name) || string.Equals(name, "Unknown", StringComparison.OrdinalIgnoreCase);

    private string T(string key, string fallback)
    {
        var s = LocalizationService.L[key];
        return s == key ? fallback : s;
    }

    private void BuildPlayersListAndBind(int? restoreIndex = null)
    {
        var unknown = T("Statistics_UnknownPlayer", "(Unknown)");
        var allPlayers = T("Statistics_AllPlayers", "All players");

        var playerNames = _allHistory
            .Select(h => IsUnknownName(h.PlayerName) ? unknown : h.PlayerName!.Trim())
            .Distinct(StringComparer.CurrentCultureIgnoreCase)
            .OrderBy(n => n, StringComparer.CurrentCultureIgnoreCase)
            .ToList();

        playerNames.Insert(0, allPlayers);

        PlayerComboBox.ItemsSource = playerNames;
        PlayerComboBox.SelectedIndex = restoreIndex is { } idx && idx >= 0 && idx < playerNames.Count ? idx : 0;
    }

    private void UpdateForSelection()
    {
        var unknown = T("Statistics_UnknownPlayer", "(Unknown)");
        var selected = PlayerComboBox.SelectedItem as string ?? "";
        List<QuizHistoryEntry> filtered;

        if (PlayerComboBox.SelectedIndex <= 0)
        {
            filtered = _allHistory;
        }
        else if (string.Equals(selected, unknown, StringComparison.CurrentCultureIgnoreCase))
        {
            filtered = _allHistory.Where(h => IsUnknownName(h.PlayerName)).ToList();
        }
        else
        {
            filtered = _allHistory.Where(h => !IsUnknownName(h.PlayerName) &&
                                              string.Equals(h.PlayerName!.Trim(), selected,
                                                  StringComparison.CurrentCultureIgnoreCase))
                                  .ToList();
        }

        UpdateStats(filtered);
    }

    private void UpdateStats(List<QuizHistoryEntry> history)
    {
        var stats = new CSharpQuizApp.ViewModels.StatisticsViewModel(history);
        
        var totalLbl   = T("Statistics_Total",     "Total quizzes played");
        var avgLbl     = T("Statistics_AvgScore",  "Average score");
        var bestLbl    = T("Statistics_BestScore", "Best score");
        var worstLbl   = T("Statistics_WorstScore","Worst score");
        var avgTimeLbl = T("Statistics_AvgTime",   "Average quiz time");

        TotalQuizzesTextBlock.Text  = $"{totalLbl}: {stats.TotalQuizzes}";
        AverageScoreTextBlock.Text  = $"{avgLbl}: {stats.AverageScorePercent}%";
        BestScoreTextBlock.Text     = $"{bestLbl}: {stats.BestScore}";
        WorstScoreTextBlock.Text    = $"{worstLbl}: {stats.WorstScore}";
        AverageTimeTextBlock.Text   = $"{avgTimeLbl}: {stats.AverageTimeSeconds} s";
    }
}
