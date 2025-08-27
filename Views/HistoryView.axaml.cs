using Avalonia.Controls;
using Avalonia.Interactivity;
using CSharpQuizApp.Data;
using CSharpQuizApp.ViewModels;

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
        => _mainWindow.NavigateToStart();

    private void ClearHistory_Click(object? sender, RoutedEventArgs e)
        => ConfirmClearOverlay.IsVisible = true;

    private void ConfirmClear_No_Click(object? sender, RoutedEventArgs e)
        => ConfirmClearOverlay.IsVisible = false;

    private void ConfirmClear_Yes_Click(object? sender, RoutedEventArgs e)
    {
        QuizDatabase.ClearHistory();
        _viewModel.LoadHistory();
        ConfirmClearOverlay.IsVisible = false;
    }
}