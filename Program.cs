using System;
using System.Globalization;
using Avalonia;
using CSharpQuizApp.Data;
using CSharpQuizApp.Localization;

namespace CSharpQuizApp;

internal static class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        var settings = UserSettings.Load();
        var lang = string.IsNullOrWhiteSpace(settings.Language) ? "pl-PL" : settings.Language;
        var ci = new CultureInfo(lang);

        // 🔥 Ustaw wszystkie możliwe pola kultury
        CultureInfo.DefaultThreadCurrentCulture = ci;
        CultureInfo.DefaultThreadCurrentUICulture = ci;
        CultureInfo.CurrentCulture = ci;
        CultureInfo.CurrentUICulture = ci;
        
        Console.WriteLine($"[DEBUG] Loaded language = {lang}");

        // 🔥 Zainicjalizuj LocalizationService od razu z kulturą z settings
        LocalizationService.Instance.SetCulture(lang);

        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }

    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
}