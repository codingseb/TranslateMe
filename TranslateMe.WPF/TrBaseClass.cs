using System;
using System.Windows;
using System.Windows.Markup;

namespace TranslateMe.WPF
{
    public abstract class TrBaseClass : MarkupExtension, IDisposable
    {
        private bool isDynamic = true;
        /// <summary>
        /// If set to true, The text will automatically be update when Current Language Change.
        /// If not the property must be updated manually.
        /// By default is set to true.
        /// </summary>
        public virtual bool IsDynamic
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

        protected FrameworkElement targetObject;
        protected DependencyProperty targetProperty;

        public TrBaseClass()
        {
            SubscribeToLanguageChange();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            targetObject = ((IProvideValueTarget)serviceProvider).TargetObject as FrameworkElement;
            targetProperty = ((IProvideValueTarget)serviceProvider).TargetProperty as DependencyProperty;

            return this;
        }

        public virtual void Dispose()
        {
            UnsubscribeFromLanguageChange();
        }

        protected virtual void SubscribeToLanguageChange()
        {
            TM.CurrentLanguageChanged += CurrentLanguageChanged;
        }

        protected virtual void UnsubscribeFromLanguageChange()
        {
            TM.CurrentLanguageChanged -= CurrentLanguageChanged;
        }

        protected abstract void CurrentLanguageChanged(object sender, TMLanguageChangedEventArgs e);
    }
}
