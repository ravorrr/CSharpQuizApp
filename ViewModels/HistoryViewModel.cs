using System.Collections.ObjectModel;
using System.Linq;
using CSharpQuizApp.Data;

namespace CSharpQuizApp.ViewModels;

public class HistoryViewModel
{
    public ObservableCollection<HistoryEntryViewModel> History { get; }

    public HistoryViewModel()
    {
        History = new ObservableCollection<HistoryEntryViewModel>();
        LoadHistory();
    }

    public void LoadHistory()
    {
        History.Clear();
        var entries = QuizDatabase.LoadHistory();
        foreach (var entry in entries)
        {
            History.Add(new HistoryEntryViewModel(entry));
        }
    }
}