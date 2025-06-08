using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CSharpQuizApp.Data;
using CSharpQuizApp.Models;

namespace CSharpQuizApp.ViewModels;

public class HistoryViewModel : INotifyPropertyChanged
{
    public ObservableCollection<QuizHistoryEntry> History { get; } = new();

    private string _sortColumn = "Date";
    private bool _sortDescending = true;

    public string SortColumn
    {
        get => _sortColumn;
        set { _sortColumn = value; OnPropertyChanged(); }
    }

    public bool SortDescending
    {
        get => _sortDescending;
        set { _sortDescending = value; OnPropertyChanged(); }
    }

    public string DateHeader => GetHeaderText("Date", "Data i godzina");
    public string PlayerHeader => GetHeaderText("PlayerName", "Gracz");
    public string TypeHeader => GetHeaderText("QuizType", "Tryb");
    public string ScoreHeader => GetHeaderText("Score", "Wynik");
    public string TimeHeader => GetHeaderText("Time", "Czas");

    public ICommand SortCommand { get; }

    public HistoryViewModel()
    {
        SortCommand = new RelayCommand(SortByColumn);
        LoadHistory();
        SortByColumn("Date");
    }

    public void LoadHistory()
    {
        History.Clear();
        var entries = QuizDatabase.LoadHistory();
        foreach (var entry in entries)
        {
            History.Add(entry);
        }

        SortByColumn(SortColumn);
    }

    private void SortByColumn(object? param)
    {
        if (param is not string column)
            return;

        if (SortColumn == column)
            SortDescending = !SortDescending;
        else
        {
            SortColumn = column;
            SortDescending = true;
        }

        IOrderedEnumerable<QuizHistoryEntry> sorted = column switch
        {
            "Date" => SortDescending
                ? History.OrderByDescending(x => x.Date)
                : History.OrderBy(x => x.Date),
            "PlayerName" => SortDescending
                ? History.OrderByDescending(x => x.PlayerName)
                : History.OrderBy(x => x.PlayerName),
            "QuizType" => SortDescending
                ? History.OrderByDescending(x => x.QuizType)
                : History.OrderBy(x => x.QuizType),
            "Score" => SortDescending
                ? History.OrderByDescending(x => x.Score)
                : History.OrderBy(x => x.Score),
            "Time" => SortDescending
                ? History.OrderByDescending(x => x.TimeInSeconds)
                : History.OrderBy(x => x.TimeInSeconds),
            _ => History.OrderByDescending(x => x.Date)
        };
        
        var sortedList = sorted.ToList();
        History.Clear();
        foreach (var entry in sortedList)
            History.Add(entry);
        
        OnPropertyChanged(nameof(DateHeader));
        OnPropertyChanged(nameof(PlayerHeader));
        OnPropertyChanged(nameof(TypeHeader));
        OnPropertyChanged(nameof(ScoreHeader));
        OnPropertyChanged(nameof(TimeHeader));
    }

    private string GetHeaderText(string column, string name)
    {
        if (SortColumn == column)
            return SortDescending ? $"{name} ▼" : $"{name} ▲";
        return name;
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? prop = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

    public class RelayCommand : ICommand
    {
        private readonly Action<object?> _execute;
        public RelayCommand(Action<object?> execute) => _execute = execute;
        public bool CanExecute(object? parameter) => true;
        public void Execute(object? parameter) => _execute(parameter);
        public event EventHandler? CanExecuteChanged;
    }
}