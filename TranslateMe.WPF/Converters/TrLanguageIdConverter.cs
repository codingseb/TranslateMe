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
    public class TrLanguageIdConverter : MarkupExtension, IValueConverter
    {
        /// <summary>
        /// To force the use of a specific identifier
        /// </summary>
        public virtual string TextId { get; set; } = null;

        /// <summary>
        /// The text to return if no text correspond to textId in the current language
        /// </summary>
        [ConstructorArgument("defaultText")]
        public string DefaultText { get; set; } = null;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return TM.Tr(TextId, DefaultText, value as string);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
