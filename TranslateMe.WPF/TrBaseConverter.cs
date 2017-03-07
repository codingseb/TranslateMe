using System;
using System.Windows.Markup;

namespace TranslateMe.WPF
{
    public abstract class TrBaseConverter : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
