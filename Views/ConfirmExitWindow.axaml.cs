using Avalonia.Controls;
using Avalonia.Interactivity;

namespace CSharpQuizApp.Views;

public partial class ConfirmExitWindow : Window
{
    public ConfirmExitWindow()
    {
        InitializeComponent();
    }

    private void Yes_Click(object? sender, RoutedEventArgs e)
    {
        Close(true);
    }

    private void No_Click(object? sender, RoutedEventArgs e)
    {
        Close(false);
    }
}