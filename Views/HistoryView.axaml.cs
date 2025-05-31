using Avalonia.Controls;
using Avalonia.Interactivity;
using CSharpQuizApp.ViewModels;

namespace CSharpQuizApp.Views;

public partial class HistoryView : UserControl
{
    private MainWindow _mainWindow;

    public HistoryView(MainWindow mainWindow)
    {
        InitializeComponent();
        _mainWindow = mainWindow;
        DataContext = new HistoryViewModel();
    }

    private void Back_Click(object? sender, RoutedEventArgs e)
    {
        _mainWindow.NavigateToStart();
    }
}