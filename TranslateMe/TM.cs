using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System
{
    /// <summary>
    /// TranslateMe most important class.
    /// For Translate something just type TM.Tr(...) method and wow... ... magic happen ;)
    /// </summary>
    public class TM : INotifyPropertyChanged
    {
        private static TM instance = null;

        public static TM Instance
        {
            get
            {
                if (instance == null)
                    instance = new TM();

                return instance;
            }
        }

        private TM()
        {}

        private ObservableCollection<string> availableLanguages = new ObservableCollection<string>();
        /// <summary>
        /// The List of all availables languages where at least one translation is present.
        /// </summary>
        public ObservableCollection<string> AvailableLanguages
        {
            get
            {
                if (availableLanguages == null)
                    availableLanguages = new ObservableCollection<string>();
                return availableLanguages;
            }
        }

        private string currentLanguage = "en";
        /// <summary>
        /// The current language used for displaying texts with TranslateMe
        /// By default if not set manually is equals to "en" (English)
        /// </summary>
        public string CurrentLanguage
        {
            get { return currentLanguage; }
            set
            {
                if(!AvailableLanguages.Contains(value))
                {
                    throw new ArgumentException($"The CurrentLanguage Property can not be set to \"{value}\". No corresponding language defined.", "CurrentLanguage");
                }

                if (!currentLanguage.Equals(value))
                {
                    TMLanguageChangingEventArgs changingArgs = new TMLanguageChangingEventArgs(currentLanguage, value);
                    TMLanguageChangedEventArgs changedArgs = new TMLanguageChangedEventArgs(currentLanguage, value);

                    CurrentLanguageChanging?.Invoke(this, changingArgs);

                    if (!changingArgs.Cancel)
                    {
                        currentLanguage = value;
                        CurrentLanguageChanged?.Invoke(this, changedArgs);
                        NotifyPropertyChanged();
                    }
                }
            }
        }

        public static event EventHandler<TMLanguageChangingEventArgs> CurrentLanguageChanging;
        public static event EventHandler<TMLanguageChangedEventArgs> CurrentLanguageChanged;

        private SortedDictionary<string, SortedDictionary<string,string>> translationsDictionary = 
            new SortedDictionary<string, SortedDictionary<string,string>>();
        internal SortedDictionary<string, SortedDictionary<string, string>> TranslationsDictionary
        {
            get
            {
                return translationsDictionary;
            }
        }

        /// <summary>
        /// Translate the given textId in current language.
        /// This method is a shortcut to Instance.Translate
        /// </summary>
        /// <param name="textId">The text to translate identifier</param>
        /// <param name="defaultText">The text to return if no text correspond to textId in the current language</param>
        /// <param name="languageId">The language id in which to get the translation. To Specify it if not CurrentLanguage</param>
        /// <returns>The translated text</returns>
        public static string Tr(string textId, string defaultText = null, string languageId = null)
        {
            return Instance.Translate(textId, defaultText, languageId);
        }

        /// <summary>
        /// Translate the given textId in current language.
        /// </summary>
        /// <param name="textId">The text to translate identifier</param>
        /// <param name="defaultText">The text to return if no text correspond to textId in the current language</param>
        /// <param name="languageId">The language id in which to get the translation. To Specify if not CurrentLanguage</param>
        /// <returns>The translated text</returns>
        public string Translate(string textId, string defaultText = null, string languageId = null)
        {
            if (string.IsNullOrEmpty(textId))
            {
                throw new InvalidOperationException("The textId argument cannot be null or empty");
            }

            if (string.IsNullOrEmpty(defaultText))
            {
                defaultText = textId;
            }

            string result = defaultText;

            if (string.IsNullOrEmpty(languageId))
            {
                languageId = Instance.CurrentLanguage;
            }

            LogMissingTranslation(textId, defaultText);

            if(TranslationsDictionary.ContainsKey(textId)
                && TranslationsDictionary[textId].ContainsKey(languageId))
            {
                result = TranslationsDictionary[textId][languageId];
            }
            
            return result;
        }

        private bool logOutMissingTranslations = false;

        /// <summary>
        /// For developpers, for developement and/or debug time.
        /// If set to <c>True</c> Log Out in a file automatically all textId asked to be translate but missing.
        /// </summary>
        public bool LogOutMissingTranslations
        {
            get { return logOutMissingTranslations; }
            set { logOutMissingTranslations = value; }
        }

        private SortedDictionary<string, SortedDictionary<string, string>> missingTranslations = new SortedDictionary<string, SortedDictionary<string, string>>();

        public SortedDictionary<string, SortedDictionary<string, string>> MissingTranslations
        {
            get { return missingTranslations; }
        }

        private string missingTranslationsFileName = Path.Combine(
            Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
            "TMMissingTranslations.json");

        private void LogMissingTranslation(string textId, string defaultText)
        {
            if (LogOutMissingTranslations)
            {
                AvailableLanguages.ToList().ForEach(delegate (string languageId)
                {
                    if(!TranslationsDictionary.ContainsKey(textId)
                        || !TranslationsDictionary[textId].ContainsKey(languageId))
                    {
                        if(!MissingTranslations.ContainsKey(textId))
                        {
                            MissingTranslations.Add(textId, new SortedDictionary<string, string>());
                        }

                        MissingTranslations[textId][languageId] = $"default text : {defaultText}";
                    }

                });

                File.WriteAllText(missingTranslationsFileName,
                    JsonConvert.SerializeObject(MissingTranslations, Formatting.Indented));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        internal void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
