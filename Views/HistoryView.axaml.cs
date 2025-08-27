using Avalonia; // Thickness
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

        var title = loc["History_Title"];
        if (title == "History_Title") title = "Historia";

        var msg = loc["History_ClearConfirm"];
        if (msg == "History_ClearConfirm") msg = "Wyczyścić całą historię?";

        var yes = loc["Quiz_Yes"]; if (yes == "Quiz_Yes") yes = "Tak";
        var no  = loc["Quiz_No"];  if (no  == "Quiz_No")  no  = "Nie";
        
        if (!yes.TrimStart().StartsWith("✅")) yes = "✅ " + yes;
        if (!no.TrimStart().StartsWith("❌"))  no  = "❌ " + no;

        var owner = this.VisualRoot as Window;
        var confirmed = await ShowConfirmAsync(owner, title, msg, yes, no);
        if (!confirmed) return;

        QuizDatabase.ClearHistory();
        _viewModel.LoadHistory();
    }

    private static Task<bool> ShowConfirmAsync(Window? owner, string title, string text, string yesText, string noText)
    {
        var tcs = new TaskCompletionSource<bool>();

        var dialog = new Window
        {
            Width = 380,
            Height = 170,
            CanResize = false,
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            Background = Brushes.Transparent,
            SystemDecorations = SystemDecorations.BorderOnly
        };
        
        var chrome = new Border
        {
            Background = new SolidColorBrush(Color.Parse("#2B2B2B")),
            CornerRadius = new CornerRadius(12),
            Padding = new Thickness(16)
        };

        var root = new StackPanel { Spacing = 12 };

        var header = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            Spacing = 8,
            HorizontalAlignment = HorizontalAlignment.Center
        };
        header.Children.Add(new TextBlock
        {
            Text = "⚠️",
            FontSize = 18,
            VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center
        });
        header.Children.Add(new TextBlock
        {
            Text = title,
            FontSize = 18,
            FontWeight = FontWeight.Bold,
            Foreground = Brushes.White,
            VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center
        });
        
        var message = new TextBlock
        {
            Text = text,
            TextWrapping = TextWrapping.Wrap,
            Foreground = Brushes.White,
            HorizontalAlignment = HorizontalAlignment.Stretch
        };

        var buttons = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            HorizontalAlignment = HorizontalAlignment.Center,
            Spacing = 12,
            Margin = new Thickness(0, 6, 0, 0)
        };

        var yesBtn = new Button
        {
            Content = yesText,
            Width = 130,
            IsDefault = true
        };
        var noBtn = new Button
        {
            Content = noText,
            Width = 130,
            IsCancel = true
        };

        yesBtn.Click += (s, e) => { tcs.TrySetResult(true); dialog.Close(); };
        noBtn.Click  += (s, e) => { tcs.TrySetResult(false); dialog.Close(); };

        buttons.Children.Add(yesBtn);
        buttons.Children.Add(noBtn);

        root.Children.Add(header);
        root.Children.Add(message);
        root.Children.Add(buttons);

        chrome.Child = root;
        dialog.Content = chrome;
        
        if (owner is null) dialog.Show();
        else _ = dialog.ShowDialog(owner);

        return tcs.Task;
    }
}
