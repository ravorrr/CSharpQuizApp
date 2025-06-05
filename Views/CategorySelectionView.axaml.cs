using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.Collections.Generic;

namespace CSharpQuizApp.Views;

public partial class CategorySelectionView : UserControl
{
    private readonly MainWindow _mainWindow;
    private readonly List<string> _categories = new()
    {
        "Geografia", "Informatyka", "Astronomia", "Historia", "Chemia", "Matematyka",
        "Biologia", "Literatura", "Języki", "Fizyka", "Kultura", "Muzyka", "Ogólna wiedza"
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

            button.Click += (s, e) => CategoryButton_Click(category);
            MainStackPanel.Children.Add(button);
        }
    }

    private void CategoryButton_Click(string category)
    {
        _mainWindow.StartCategoryQuiz(category);
    }
    
    private void BackToMenu_Click(object? sender, RoutedEventArgs e)
    {
        _mainWindow.NavigateToStart();
    }
}