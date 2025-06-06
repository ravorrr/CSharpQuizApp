using System.Collections.ObjectModel;
using CSharpQuizApp.Data;
using CSharpQuizApp.Models;

namespace CSharpQuizApp.ViewModels;

public class HistoryViewModel
{
    public ObservableCollection<QuizHistoryEntry> History { get; }

    public HistoryViewModel()
    {
        History = new ObservableCollection<QuizHistoryEntry>();
        LoadHistory();
    }

    public void LoadHistory()
    {
        History.Clear();
        var entries = QuizDatabase.LoadHistory();
        foreach (var entry in entries)
        {
            History.Add(entry);
        }
    }
}