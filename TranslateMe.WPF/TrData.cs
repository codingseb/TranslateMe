using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace TranslateMe.WPF
{
    /// <summary>
    /// This class is used as viewModel to bind  to DependencyProperties
    /// Is use by Tr MarkupExtension to dynamically update the translation when current language changed
    /// </summary>
    public class TrData : INotifyPropertyChanged
    {
        public TrData()
        {
            WeakEventManager<TM, TMLanguageChangedEventArgs>.AddHandler(TM.Instance, "CurrentLanguageChanged", CurrentLanguageChanged);
        }

        ~TrData()
        {
            WeakEventManager<TM, TMLanguageChangedEventArgs>.RemoveHandler(TM.Instance, "CurrentLanguageChanged", CurrentLanguageChanged);
        }

        /// <summary>
        /// To force the use of a specific identifier
        /// </summary>
        public string TextId { get; set; }

        /// <summary>
        /// The text to return if no text correspond to textId in the current language
        /// </summary>
        public string DefaultText { get; set; }
        public string LanguageId { get; set; }

        /// <summary>
        /// When the current Language changed update the binding (Call OnPropertyChanged)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CurrentLanguageChanged(object sender, TMLanguageChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Translation));
        }

        public string Translation
        {
            get
            {
                return TM.Tr(TextId, DefaultText, LanguageId);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
