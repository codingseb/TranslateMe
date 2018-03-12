using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace TranslateMe.WPF
{
    /// <summary>
    /// Translate With TranslateMe
    /// </summary>
    public class Tr : MarkupExtension, IDisposable
    {
        private DependencyObject targetObject;
        private DependencyProperty targetProperty;
        private string defaultText = null;

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
        /// <param name="textId">To force the use of a specific identifier</param>
        public Tr(string textId)
        {
            SubscribeToLanguageChange();
            TextId = textId;
        }

        /// <summary>
        /// Translate Me : Translate in the current language the given textId
        /// </summary>
        /// <param name="textId">To force the use of a specific identifier</param>
        /// <param name="defaultText">The text to return if no text correspond to textId in the current language</param>
        public Tr(string textId, string defaultText) : base()
        {
            SubscribeToLanguageChange();
            TextId = textId;
            DefaultText = defaultText;
        }

        /// <summary>
        /// To force the use of a specific identifier
        /// </summary>
        [ConstructorArgument("textId")]
        public virtual string TextId { get; set; } = null;

        /// <summary>
        /// The text to return if no text correspond to textId in the current language
        /// </summary>
        public string DefaultText
        {
            get { return defaultText; }
            set
            {
                defaultText = value.Replace("[apos]", "'");
            }
        }

        /// <summary>
        /// The language id in which to get the translation. To Specify if not CurrentLanguage
        /// </summary>
        public string LanguageId { get; set; } = null;

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
            TM.CurrentLanguageChanged += CurrentLanguageChanged;
        }

        protected void UnsubscribeFromLanguageChange()
        {
            TM.CurrentLanguageChanged -= CurrentLanguageChanged;
        }

        private void CurrentLanguageChanged(object sender, TMLanguageChangedEventArgs e)
        {
            if (IsDynamic && targetObject != null && targetProperty != null)
            {
                targetObject.SetValue(targetProperty, TM.Tr(TextId, DefaultText, LanguageId));
            }
        }

        public virtual void Dispose()
        {
            UnsubscribeFromLanguageChange();
        }

        /// <summary>
        /// Translation In Xaml
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                return DefaultText ?? TextId ?? "Translated Text";

            if (serviceProvider.GetService(typeof(IProvideValueTarget)) is IProvideValueTarget target)
            {
                if (target.TargetObject.GetType().FullName == "System.Windows.SharedDp" || target.TargetObject is Setter)
                    return this;

                try
                {

                    targetObject = target.TargetObject as DependencyObject;
                    targetProperty = target.TargetProperty as DependencyProperty;

                    if (string.IsNullOrEmpty(TextId))
                    {
                        if (targetObject != null && targetProperty != null)
                        {
                            string context = targetObject.GetContextByName();
                            string obj = targetObject.FormatForTextId();
                            string property = targetProperty.ToString();

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
            }

            return TM.Tr(TextId, DefaultText, LanguageId);
        }
    }
}
