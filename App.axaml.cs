using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using CSharpQuizApp.Localization;
using CSharpQuizApp.Views;

namespace CSharpQuizApp;

public partial class App : Application
{
    public override void Initialize() => AvaloniaXamlLoader.Load(this);

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Subskrybuj zmianę kultury: po kliknięciu flagi przebuduj całe okno
            LocalizationService.Instance.CultureChanged += () =>
            {
                var old = desktop.MainWindow;
                var fresh = new MainWindow();
                desktop.MainWindow = fresh;
                fresh.Show();
                old?.Close();
            };

            // Teraz utwórz pierwsze okno (na bazie już ustawionej kultury)
            desktop.MainWindow = new MainWindow();
        }

        base.OnFrameworkInitializationCompleted();
    }
}