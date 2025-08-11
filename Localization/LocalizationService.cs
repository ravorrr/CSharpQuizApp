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

        private LocalizationService()
        {
            var baseName = $"{typeof(LocalizationService).Namespace}.Strings";
            _rm = new ResourceManager(baseName, Assembly.GetExecutingAssembly());
            Localizer = new Localizer(_rm, CultureInfo.CurrentUICulture);
        }

        public void SetCulture(string cultureName)
        {
            var culture = new CultureInfo(cultureName);
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;
            Localizer.SetCulture(culture);
        }
    }
}