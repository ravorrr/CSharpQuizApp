using Avalonia.Controls;
using Avalonia.Interactivity;
using CSharpQuizApp.ViewModels;
using CSharpQuizApp.Data;

namespace CSharpQuizApp.Views;

public partial class HistoryView : UserControl
{
    private readonly MainWindow _mainWindow;
    private readonly HistoryViewModel _viewModel;

    public HistoryView(MainWindow mainWindow)
    {
        InitializeComponent();
        _mainWindow = mainWindow;
        _viewModel = new HistoryViewModel();
        DataContext = _viewModel;
    }

    private void Back_Click(object? sender, RoutedEventArgs e)
    {
        _mainWindow.NavigateToStart();
    }

    private void ClearHistory_Click(object? sender, RoutedEventArgs e)
    {
        QuizDatabase.ClearHistory();
        _viewModel.LoadHistory();
    }
}