using System;
using System.Globalization;
using System.Windows.Data;

namespace TranslateMe.WPF
{
    /// <summary>
    /// Converter to Translate a specific TextId in the Binding LanguageId.
    /// If Translation don't exist return DefaultText
    /// </summary>
    public class TrLanguageIdConverter : TrBaseConverter, IValueConverter
    {
        public string DefaultText { get; set; }
        public string TextId { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return TM.Tr(TextId, DefaultText, value as string);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
