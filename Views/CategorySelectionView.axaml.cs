using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Collections.Generic;

namespace CSharpQuizApp.Views;

public partial class CategorySelectionView : UserControl
{
    private readonly MainWindow _mainWindow;
    private readonly List<(string Emoji, string Name)> _categories = new()
    {
        ("🌍", "Geografia"),
        ("💻", "Informatyka"),
        ("🌌", "Astronomia"),
        ("📜", "Historia"),
        ("⚗️", "Chemia"),
        ("➗", "Matematyka"),
        ("🧬", "Biologia"),
        ("📚", "Literatura"),
        ("🗣️", "Języki"),
        ("🔬", "Fizyka"),
        ("🎭", "Kultura"),
        ("🎵", "Muzyka"),
        ("🏎️", "Motoryzacja"),
        ("🏅", "Sport"),
        ("🤖", "Technologia")
    };

    public CategorySelectionView(MainWindow mainWindow)
    {
        InitializeComponent();
        _mainWindow = mainWindow;

        foreach (var (emoji, name) in _categories)
        {
            var button = new Button
            {
                Content = $"{emoji} {name}",
                Width = 230,
                Height = 54,
                Margin = new Thickness(12, 10, 12, 10),
                FontSize = 18,
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Stretch,
                Cursor = new Avalonia.Input.Cursor(Avalonia.Input.StandardCursorType.Hand)
            };
            button.Click += CategoryButton_Click;
            CategoryGrid.Children.Add(button);
        }
    }

    private void CategoryButton_Click(object? sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Content is string content)
        {
            var category = content.Substring(content.IndexOf(' ') + 1);
            _mainWindow.StartCategoryQuiz(category);
        }
    }

    private void BackToMenu_Click(object? _, RoutedEventArgs __)
    {
        _mainWindow.NavigateToStart();
    }
}