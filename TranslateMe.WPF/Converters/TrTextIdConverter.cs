using System;
using System.Linq;
using System.Globalization;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using System.Xaml;

namespace TranslateMe.WPF
{
    /// <summary>
    /// Converter to Translate a the binding textId (In CurrentLanguage or if specified in LanguageId)
    /// If Translation don't exist return DefaultText
    /// Not usable in TwoWay Binding mode.
    /// </summary>
    public class TrTextIdConverter : MarkupExtension, IValueConverter
    {
        public TrTextIdConverter()
        {
            SubscribeToLanguageChange();
        }

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
            return string.IsNullOrEmpty(textId) ? "" : TM.Tr(textId, DefaultText, LanguageId);
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
            }
            catch { }

            return this;
        }

        private bool isDynamic = true;
        /// <summary>
        /// If set to true, The text will automatically be update when Current Language Change.
        /// If not the property must be updated manually.
        /// By default is set to true.
        /// </summary>
        public bool IsDynamic
        {
            get { return isDynamic; }
            set
            {
                if (isDynamic != value)
                {
                    isDynamic = value;

                    if (isDynamic)
                    {
                        SubscribeToLanguageChange();
                    }
                    else
                    {
                        UnsubscribeFromLanguageChange();
                    }
                }
            }
        }

        protected void SubscribeToLanguageChange()
        {
            WeakEventManager<TM, TMLanguageChangedEventArgs>.AddHandler(TM.Instance, nameof(TM.Instance.CurrentLanguageChanged), CurrentLanguageChanged);
        }

        protected void UnsubscribeFromLanguageChange()
        {
            WeakEventManager<TM, TMLanguageChangedEventArgs>.RemoveHandler(TM.Instance, nameof(TM.Instance.CurrentLanguageChanged), CurrentLanguageChanged);
        }

        private void CurrentLanguageChanged(object sender, TMLanguageChangedEventArgs e)
        {
            if (IsDynamic && xamlTargetObject != null && xamlDependencyProperty != null)
            {
                xamlTargetObject.GetBindingExpression(xamlDependencyProperty)?.UpdateTarget();
            }
        }

        public virtual void Dispose()
        {
            UnsubscribeFromLanguageChange();
        }
    }
}
