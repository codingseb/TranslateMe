using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace TranslateMe.WPF
{
    public abstract class TrWithTextIdAndDefaultTextProperty : TrBaseClass
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


        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            try
            {
                base.ProvideValue(serviceProvider);

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

            return this;
        }
    }
}
