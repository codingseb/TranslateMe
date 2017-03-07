using System;
using System.Windows;
using System.Windows.Markup;

[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "TranslateMe.WPF")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2007/xaml/presentation", "TranslateMe.WPF")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2008/xaml/presentation", "TranslateMe.WPF")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2009/xaml/presentation", "TranslateMe.WPF")]

namespace TranslateMe.WPF
{
    /// <summary>
    /// Translate With TranslateMe
    /// </summary>
    [MarkupExtensionReturnType(typeof(string))]
    public class Tr : MarkupExtension, IDisposable
    {
        /// <summary>
        /// Translate the current Property in the current language
        /// The Default TextId is "CurrentNamespace.CurrentClass.CurrentProperty"
        /// </summary>
        public Tr()
        {
            SubscribeToLanguageChange();
        }

        /// <summary>
        /// Translate the current Property in the current language
        /// The Default TextId is "CurrentNamespace.CurrentClass.CurrentProperty"
        /// </summary>
        /// <param name="defaultText">The text to return if no text correspond to textId in the current language</param>
        public Tr(string defaultText)
        {
            SubscribeToLanguageChange();
            this.defaultText = defaultText;
        }

        /// <summary>
        /// Translate Me : Translate in the current language the given textId
        /// </summary>
        /// <param name="defaultText">The text to return if no text correspond to textId in the current language</param>
        /// <param name="textId">To force the use of a specific identifier</param>
        public Tr(string defaultText, string textId)
        {
            SubscribeToLanguageChange();
            this.textId = textId;
            this.defaultText = defaultText;
        }

        public void Dispose()
        {
            UnsubscribeFromLanguageChange();
        }

        private void SubscribeToLanguageChange()
        {
            TM.CurrentLanguageChanged += CurrentLanguageChanged;
        }

        private void UnsubscribeFromLanguageChange()
        {
            TM.CurrentLanguageChanged -= CurrentLanguageChanged;
        }

        private string defaultText = null;
        /// <summary>
        /// The text to return if no text correspond to textId in the current language
        /// </summary>
        [ConstructorArgument("defaultText")]
        public string DefaultText
        {
            get { return defaultText; }
            set { defaultText = value; }
        }

        private string textId = null;
        /// <summary>
        /// To force the use of a specific identifier
        /// The Default TextId is "CurrentNamespace.CurrentClass.CurrentProperty"
        /// </summary>
        public string TextId
        {
            get { return textId; }
            set { textId = value; }
        }
        
        private string languageId = null;
        /// <summary>
        /// The language id in which to get the translation. To Specify if not CurrentLanguage
        /// </summary>
        public string LanguageId
        {
            get { return languageId; }
            set { languageId = value; }
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

                    if(isDynamic)
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

        private FrameworkElement targetObject;
        private DependencyProperty targetProperty;

        private void CurrentLanguageChanged(object sender, TMLanguageChangedEventArgs e)
        {
            if (IsDynamic && targetObject != null && targetProperty != null)
            {
                targetObject.SetValue(targetProperty, TM.Tr(TextId, DefaultText, LanguageId));
            }
        }

        /// <summary>
        /// Translation In Xaml
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            try
            {
                targetObject = ((IProvideValueTarget)serviceProvider).TargetObject as FrameworkElement;
                targetProperty = ((IProvideValueTarget)serviceProvider).TargetProperty as DependencyProperty;

                if (string.IsNullOrEmpty(textId))
                {
                    if (targetObject != null && targetProperty != null)
                    {
                        string context = targetObject.GetContextByName();
                        string obj = targetObject.FormatForTextId();
                        string property = targetProperty.ToString();

                        TextId = $"{context}.{obj}.{property}";
                    }
                    else if(!string.IsNullOrEmpty(DefaultText))
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

            return TM.Tr(TextId, DefaultText, LanguageId);
        }

    }
}
