using System.Windows;
using System.Windows.Controls;

namespace TranslateMe.WPF
{
    public static class VisualTreeHelper
    {
        public static string GetContextByName(this FrameworkElement fe)
        {
            string result = "";

            if (fe != null)
            {
                if(fe is UserControl || fe is Window)
                {
                    result = fe.FormatForTextId(true);
                }
                else
                {
                    result = GetContextByName(fe.Parent as FrameworkElement);
                }
            }

            return string.IsNullOrEmpty(result) ? fe.FormatForTextId(true) : result;
        }

        public static string FormatForTextId(this FrameworkElement fe, bool typeFullName = false)
        {
            return $"{fe.Name}[{(typeFullName ? fe.GetType().FullName : fe.GetType().Name)}]";
        }
    }
}
