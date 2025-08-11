using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using CSharpQuizApp.Views;
using System;
using CSharpQuizApp.Utils;
using CSharpQuizApp.Localization;
using CSharpQuizApp.Data;

namespace CSharpQuizApp;

public class App : Application
{
    public override void Initialize() => AvaloniaXamlLoader.Load(this);

    public override void OnFrameworkInitializationCompleted()
    {
        Data.QuizDatabase.Initialize();
        
        AppDomain.CurrentDomain.UnhandledException += (_, e) =>
        {
            if (e.ExceptionObject is Exception ex) Logger.LogError(ex);
        };
        
        var settings = UserSettings.Load();
        try
        {
            LocalizationService.Instance.SetCulture(settings.Language);
        }
        catch
        {
            LocalizationService.Instance.SetCulture("pl-PL");
            settings.Language = "pl-PL";
            settings.Save();
        }

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            desktop.MainWindow = new MainWindow();

        base.OnFrameworkInitializationCompleted();
    }
}