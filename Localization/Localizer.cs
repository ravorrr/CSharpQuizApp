using System.ComponentModel;
using System.Globalization;
using System.Resources;

namespace QuizApp.Localization
{
    public class Localizer : INotifyPropertyChanged
    {
        private readonly ResourceManager _rm;
        private CultureInfo _culture;

        public Localizer(ResourceManager rm, CultureInfo culture)
        {
            _rm = rm;
            _culture = culture;
        }

        public string this[string key] => _rm.GetString(key, _culture) ?? key;

        public void SetCulture(CultureInfo culture)
        {
            _culture = culture;
            
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Item[]"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(string.Empty));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}