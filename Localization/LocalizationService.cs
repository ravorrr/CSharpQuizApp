using System;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace CSharpQuizApp.Localization
{
    public sealed class LocalizationService
    {
        public static LocalizationService Instance { get; } = new();

        private readonly ResourceManager _rm;
        public Localizer Localizer { get; }
        public static Localizer L => Instance.Localizer;

        public event Action? CultureChanged;

        private LocalizationService()
        {
            var baseName = $"{typeof(LocalizationService).Namespace}.Strings";
            _rm = new ResourceManager(baseName, Assembly.GetExecutingAssembly());
            Localizer = new Localizer(_rm, CultureInfo.CurrentUICulture);
        }

        public void SetCulture(string cultureName)
        {
            var ci = new CultureInfo(cultureName);

            CultureInfo.DefaultThreadCurrentCulture = ci;
            CultureInfo.DefaultThreadCurrentUICulture = ci;
            CultureInfo.CurrentCulture = ci;
            CultureInfo.CurrentUICulture = ci;

            Localizer.SetCulture(ci);
            
            CultureChanged?.Invoke();
        }
    }
}