using System;
using System.Globalization;
using System.Windows.Data;

namespace TranslateMe.WPF
{
    public class TrLanguageIdConverter : TrBaseConverter, IValueConverter
    {
        public string DefaultText { get; set; }
        public string TextId { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
