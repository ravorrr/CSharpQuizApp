using Avalonia; // dla Thickness
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;
using CSharpQuizApp.Data;
using CSharpQuizApp.Localization;
using CSharpQuizApp.ViewModels;
using System.Threading.Tasks;

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

    private async void ClearHistory_Click(object? sender, RoutedEventArgs e)
    {
        var loc = LocalizationService.L;

        // Teksty z RESX (+ fallbacki)
        var msg = loc["History_ClearConfirm"];
        if (msg == "History_ClearConfirm") msg = "Wyczyścić całą historię?";
        var yes = loc["Quiz_Yes"]; if (yes == "Quiz_Yes") yes = "Tak";
        var no  = loc["Quiz_No"];  if (no  == "Quiz_No")  no  = "Nie";

        var owner = this.VisualRoot as Window;
        var confirmed = await ShowConfirmAsync(owner, msg, yes, no);
        if (!confirmed) return;

        QuizDatabase.ClearHistory();
        _viewModel.LoadHistory();
    }
    
    private static Task<bool> ShowConfirmAsync(Window? owner, string text, string yesText, string noText)
    {
        var tcs = new TaskCompletionSource<bool>();

        var dialog = new Window
        {
            Width = 360,
            Height = 160,
            CanResize = false,
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            Background = new SolidColorBrush(Color.Parse("#2B2B2B")),
            SystemDecorations = SystemDecorations.BorderOnly
        };
        
        var root = new StackPanel { Spacing = 12, Margin = new Thickness(16) };

        root.Children.Add(new TextBlock
        {
            Text = text,
            TextWrapping = TextWrapping.Wrap,
            Foreground = Brushes.White,
            HorizontalAlignment = HorizontalAlignment.Stretch
        });

        var buttons = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            HorizontalAlignment = HorizontalAlignment.Center,
            Spacing = 12
        };

        var yesBtn = new Button { Content = yesText, Width = 110 };
        var noBtn  = new Button { Content = noText,  Width = 110 };

        yesBtn.Click += (s, e) => { tcs.TrySetResult(true); dialog.Close(); };
        noBtn.Click  += (s, e) => { tcs.TrySetResult(false); dialog.Close(); };

        buttons.Children.Add(yesBtn);
        buttons.Children.Add(noBtn);

        root.Children.Add(buttons);
        dialog.Content = root;

        if (owner is null) dialog.Show();
        else _ = dialog.ShowDialog(owner);

        return tcs.Task;
    }
}
