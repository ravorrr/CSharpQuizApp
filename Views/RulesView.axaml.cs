using Avalonia.Controls;
using Avalonia.Interactivity;

namespace CSharpQuizApp.Views;

public partial class RulesView : UserControl
{
    private readonly MainWindow _mainWindow;

    public RulesView(MainWindow mainWindow)
    {
        InitializeComponent();
        _mainWindow = mainWindow;
    }

    private void BackToMenu_Click(object? sender, RoutedEventArgs e)
    {
        _mainWindow.NavigateToStart();
    }
}