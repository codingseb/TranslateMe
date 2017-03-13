using System;
using System.Globalization;
using System.Windows.Data;

namespace TranslateMe.WPF
{
    /// <summary>
    /// Converter to Translate a the binding textId (In CurrentLanguage or if specified in LanguageId)
    /// If Translation don't exist return DefaultText
    /// Not usable in TwoWay Binding mode.
    /// </summary>
    public class TrTextIdConverter : TrBaseClass, IValueConverter
    {
        /// <summary>
        /// The text to return if no text correspond to textId in the current language
        /// </summary>
        public string DefaultText { get; set; } = null;

        /// <summary>
        /// The language id in which to get the translation. To Specify if not CurrentLanguage
        /// </summary>
        public string LanguageId { get; set; } = null;

        private string textId = "";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            textId = value as string;
            return TM.Tr(textId, DefaultText, LanguageId);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        protected override void CurrentLanguageChanged(object sender, TMLanguageChangedEventArgs e)
        {
            if (IsDynamic && targetObject != null && targetProperty != null)
            {
                targetObject.SetValue(targetProperty, TM.Tr(textId, DefaultText, LanguageId));
            }
        }
    }
}
