using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows;
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

        FrameworkElement xamlTargetObject;
        DependencyProperty xamlDependencyProperty;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            try
            {
                var xamlContext = serviceProvider.GetType()
                    .GetRuntimeFields().ToList()
                    .Find(f => f.Name.Equals("_xamlContext"))
                    .GetValue(serviceProvider);

                xamlTargetObject = xamlContext.GetType()
                    .GetProperty("GrandParentInstance")
                    .GetValue(xamlContext) as FrameworkElement;

                var xamlProperty = xamlContext.GetType()
                    .GetProperty("GrandParentProperty")
                    .GetValue(xamlContext);

                xamlDependencyProperty = xamlProperty.GetType()
                    .GetProperty("DependencyProperty")
                    .GetValue(xamlProperty) as DependencyProperty;

                if (string.IsNullOrEmpty(TextId))
                {
                    if (xamlTargetObject != null && xamlDependencyProperty != null)
                    {
                        string context = xamlTargetObject.GetContextByName();
                        string obj = xamlTargetObject.FormatForTextId();
                        string property = xamlDependencyProperty.ToString();

                        TextId = $"{context}.{obj}.{property}";
                    }
                    else if (!string.IsNullOrEmpty(DefaultText))
                    {
                        TextId = DefaultText;
                    }
                }
            }
            catch (InvalidCastException)
            {
                // For Xaml Design Time
                TextId = Guid.NewGuid().ToString();
            }
            return this;
        }
    }
}
