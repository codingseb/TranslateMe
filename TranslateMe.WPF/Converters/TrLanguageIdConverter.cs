using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace TranslateMe.WPF
{
    /// <summary>
    /// Converter to Translate a specific TextId in the Binding LanguageId.
    /// If Translation don't exist return DefaultText.
    /// Not usable in TwoWay Binding mode.
    /// </summary>
    public class TrLanguageIdConverter : TrWithTextIdAndDefaultTextProperty, IValueConverter
    {
        public TrLanguageIdConverter()
        {
            IsDynamic = false;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return TM.Tr(TextId, DefaultText, value as string);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        protected override void CurrentLanguageChanged(object sender, TMLanguageChangedEventArgs e)
        {
        }
    }
}
