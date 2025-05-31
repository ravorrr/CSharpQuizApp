using System.Collections.ObjectModel;
using System.Linq;
using CSharpQuizApp.Data;

namespace CSharpQuizApp.ViewModels;

public class HistoryViewModel
{
    public ObservableCollection<HistoryEntryViewModel> History { get; }

    public HistoryViewModel()
    {
        var entries = QuizDatabase.LoadHistory();
        History = new ObservableCollection<HistoryEntryViewModel>(
            entries.Select(e => new HistoryEntryViewModel(e))
        );
    }
}