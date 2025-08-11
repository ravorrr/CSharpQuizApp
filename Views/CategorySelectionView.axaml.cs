using Avalonia.Controls;
using Avalonia.Interactivity;

namespace CSharpQuizApp.Views;

public partial class CategorySelectionView : UserControl
{
    private readonly MainWindow _mainWindow;

    public CategorySelectionView(MainWindow mainWindow)
    {
        InitializeComponent();
        _mainWindow = mainWindow;
    }

    private void Category_Click(object? sender, RoutedEventArgs e)
    {
        if (sender is Button btn && btn.Tag is string categoryKey && !string.IsNullOrWhiteSpace(categoryKey))
        {
            _mainWindow.StartCategoryQuiz(categoryKey);
        }
    }

    private void BackToMenu_Click(object? _, RoutedEventArgs __)
        => _mainWindow.NavigateToStart();
}