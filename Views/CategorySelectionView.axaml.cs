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
        if (sender is Button btn && btn.Content is StackPanel stack &&
            stack.Children.Count == 2 && stack.Children[1] is TextBlock nameBlock)
        {
            var category = nameBlock.Text ?? string.Empty;
            _mainWindow.StartCategoryQuiz(category);
        }
    }

    private void BackToMenu_Click(object? _, RoutedEventArgs __)
    {
        _mainWindow.NavigateToStart();
    }
}