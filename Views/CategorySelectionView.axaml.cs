using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Collections.Generic;

namespace CSharpQuizApp.Views;

public partial class CategorySelectionView : UserControl
{
    private readonly MainWindow _mainWindow;
    private readonly List<string> _categories = new()
    {
        "Geografia", "Informatyka", "Astronomia", "Historia", "Chemia",
        "Matematyka", "Biologia", "Literatura", "Języki", "Fizyka",
        "Kultura", "Muzyka", "Motoryzacja", "Sport", "Technologia"
    };

    public CategorySelectionView(MainWindow mainWindow)
    {
        InitializeComponent();
        _mainWindow = mainWindow;

        foreach (var category in _categories)
        {
            var button = new Button
            {
                Content = category,
                Width = 250,
                Height = 40,
                Margin = new Thickness(0, 5, 0, 5),
                Cursor = new Avalonia.Input.Cursor(Avalonia.Input.StandardCursorType.Hand)
            };

            button.Click += CategoryButton_Click;
            MainStackPanel.Children.Add(button);
        }
    }

    private void CategoryButton_Click(object? sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Content is string category)
        {
            _mainWindow.StartCategoryQuiz(category);
        }
    }
    
    private void BackToMenu_Click(object? _, RoutedEventArgs __)
    {
        _mainWindow.NavigateToStart();
    }
}