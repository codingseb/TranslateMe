using System;
using System.Windows;
using System.Windows.Markup;

namespace TranslateMe.WPF
{
    /// <summary>
    /// Translate With TranslateMe
    /// </summary>
    [MarkupExtensionReturnType(typeof(string))]
    public class Tr : TrWithTextIdAndDefaultTextProperty
    {
        /// <summary>
        /// Translate the current Property in the current language
        /// The Default TextId is "CurrentNamespace.CurrentClass.CurrentProperty"
        /// </summary>
        public Tr() : base()
        {}

        /// <summary>
        /// Translate the current Property in the current language
        /// The Default TextId is "CurrentNamespace.CurrentClass.CurrentProperty"
        /// </summary>
        /// <param name="defaultText">The text to return if no text correspond to textId in the current language</param>
        public Tr(string defaultText) : base()
        {
            this.DefaultText = defaultText;
        }

        /// <summary>
        /// Translate Me : Translate in the current language the given textId
        /// </summary>
        /// <param name="defaultText">The text to return if no text correspond to textId in the current language</param>
        /// <param name="textId">To force the use of a specific identifier</param>
        public Tr(string defaultText, string textId) : base()
        {
            this.TextId = textId;
            this.DefaultText = defaultText;
        }
        
        /// <summary>
        /// The language id in which to get the translation. To Specify if not CurrentLanguage
        /// </summary>
        public string LanguageId { get; set; } = null;

        protected override void CurrentLanguageChanged(object sender, TMLanguageChangedEventArgs e)
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
            base.ProvideValue(serviceProvider);

            return TM.Tr(TextId, DefaultText, LanguageId);
        }
    }
}
