using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using CSharpQuizApp.Utils;
using System;

namespace CSharpQuizApp.Views;

public partial class StartView : UserControl
{
    private readonly MainWindow _mainWindow;
    private UserSettings _userSettings;

    public StartView(MainWindow mainWindow)
    {
        InitializeComponent();
        _mainWindow = mainWindow;
        _userSettings = UserSettings.Load();

        UpdateWelcomeMessage();
    }

    private void UpdateWelcomeMessage()
    {
        if (!string.IsNullOrWhiteSpace(_userSettings.PlayerName))
        {
            WelcomeTextBlock.Text = $"Witaj, {_userSettings.PlayerName}!";
        }
        else
        {
            WelcomeTextBlock.Text = "Kliknij tutaj, aby wpisać imię";
        }
    }

    private void WelcomeTextBlock_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        NameEditBox.Text = _userSettings.PlayerName;
        NameEditBox.IsVisible = true;
        WelcomeTextBlock.IsVisible = false;
        NameEditBox.Focus();
        NameEditBox.CaretIndex = NameEditBox.Text?.Length ?? 0;
    }

    private void NameEditBox_KeyUp(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
            SaveAndSwitchBack();
    }

    private void NameEditBox_LostFocus(object? sender, RoutedEventArgs e)
    {
        SaveAndSwitchBack();
    }

    private void SaveAndSwitchBack()
    {
        var name = NameEditBox.Text?.Trim() ?? "";
        _userSettings.PlayerName = name;
        _userSettings.Save();

        UpdateWelcomeMessage();

        NameEditBox.IsVisible = false;
        WelcomeTextBlock.IsVisible = true;
    }

    private void StartQuiz_Click(object? sender, RoutedEventArgs e)
    {
        _mainWindow.NavigateToModeSelection();
    }

    private void ShowRules_Click(object? sender, RoutedEventArgs e)
    {
        RulesTextBlock.IsVisible = !RulesTextBlock.IsVisible;
    }
}