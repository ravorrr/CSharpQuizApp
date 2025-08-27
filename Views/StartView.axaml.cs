using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using CSharpQuizApp.Data;
using CSharpQuizApp.Localization;
using System;
using System.ComponentModel;

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
        
        LocalizationService.Instance.Localizer.PropertyChanged += OnLocalizerPropertyChanged;

        UpdateFlagVisuals();
        UpdateWelcomeMessage();
    }

    private void OnLocalizerPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (string.IsNullOrEmpty(e.PropertyName) || e.PropertyName == "Item[]")
        {
            UpdateWelcomeMessage();
            UpdateFlagVisuals();
        }
    }

    private static Bitmap? LoadAssetBitmap(string avaresUri)
    {
        try
        {
            using var s = AssetLoader.Open(new Uri(avaresUri));
            return new Bitmap(s);
        }
        catch { return null; }
    }

    private void UpdateFlagVisuals()
    {
        var lang = (_userSettings.Language ?? "pl-PL").ToLowerInvariant();

        string A(string name) => $"avares://CSharpQuizApp/Assets/Flags/{name}";
        var plUri = lang.StartsWith("pl") ? A("pl.png") : A("pl.gray.png");
        var enUri = lang.StartsWith("en") ? A("en.png") : A("en.gray.png");

        if (ImgPl is not null) ImgPl.Source = LoadAssetBitmap(plUri);
        if (ImgEn is not null) ImgEn.Source = LoadAssetBitmap(enUri);
    }

    private static string NormalizeCulture(string tag) => tag switch
    {
        "pl" or "PL" => "pl-PL",
        "en" or "EN" => "en-US",
        _ => tag
    };

    private void LanguageButton_Click(object? sender, RoutedEventArgs e)
    {
        if (sender is not Button btn || btn.Tag is not string raw) return;

        var culture = NormalizeCulture(raw);
        
        _userSettings.Language = culture;
        _userSettings.Save();
        
        LocalizationService.Instance.SetCulture(culture);
        
        UpdateFlagVisuals();
        UpdateWelcomeMessage();
    }

    private void UpdateWelcomeMessage()
    {
        var name = _userSettings.PlayerName;
        var loc = LocalizationService.L;

        var enterName = loc["Start_EnterName"];
        var withName  = loc["Start_Welcome_WithName"];

        if (withName == "Start_Welcome_WithName")
            withName = "Witaj, {0}!";

        var hasName = !string.IsNullOrWhiteSpace(name) &&
                      !name.Equals("unknown", StringComparison.OrdinalIgnoreCase);

        if (WelcomeTextBlock is not null)
            WelcomeTextBlock.Text = hasName
                ? string.Format(System.Globalization.CultureInfo.CurrentUICulture, withName, name)
                : enterName;
    }

    private void WelcomeTextBlock_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (NameEditBox is null || WelcomeTextBlock is null) return;

        NameEditBox.Text = _userSettings.PlayerName.Equals("unknown", StringComparison.OrdinalIgnoreCase)
            ? ""
            : _userSettings.PlayerName;

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
        if (NameEditBox is null || WelcomeTextBlock is null) return;

        var name = NameEditBox.Text?.Trim();
        if (string.IsNullOrWhiteSpace(name))
            name = "Unknown";

        _userSettings.PlayerName = name;
        _userSettings.Save();

        UpdateWelcomeMessage();
        NameEditBox.IsVisible = false;
        WelcomeTextBlock.IsVisible = true;
    }

    private void StartQuiz_Click(object? sender, RoutedEventArgs e)
        => _mainWindow.NavigateToModeSelection();

    private void ShowRules_Click(object? sender, RoutedEventArgs e)
        => _mainWindow.NavigateToRules();

    private void ShowHistory_Click(object? sender, RoutedEventArgs e)
        => _mainWindow.NavigateToHistory();

    private void ShowStatistics_Click(object? sender, RoutedEventArgs e)
        => _mainWindow.NavigateToStatistics();

    private void FocusCatcher_PointerPressed(object? sender, PointerPressedEventArgs e)
        => Focus();
}
