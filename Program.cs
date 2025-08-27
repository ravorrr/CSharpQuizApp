using System;
using System.Globalization;
using Avalonia;
using QuizApp.Data;
using QuizApp.Localization;

namespace QuizApp;

internal static class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        var settings = UserSettings.Load();
        var lang = string.IsNullOrWhiteSpace(settings.Language) ? "pl-PL" : settings.Language;
        
        var ci = new CultureInfo(lang);
        CultureInfo.DefaultThreadCurrentCulture = ci;
        CultureInfo.DefaultThreadCurrentUICulture = ci;
        CultureInfo.CurrentCulture = ci;
        CultureInfo.CurrentUICulture = ci;

#if DEBUG
        // ReSharper disable once LocalizableElement
        Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "[DEBUG] Loaded language = {0}", lang));
#endif
        
        LocalizationService.Instance.SetCulture(lang);

        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }

    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
}