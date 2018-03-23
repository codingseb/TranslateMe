using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TranslateMe
{
    public class TMLanguagesLoader
    {
        TM tmInstance = null;

        public TMLanguagesLoader(TM tmInstance)
        {
            this.tmInstance = tmInstance;
        }

        public List<ITMFileLanguageLoader> FileLanguageLoaders { get; set; } = new List<ITMFileLanguageLoader>()
        {
            new TMJsonFileLanguageLoader(),
        };

        /// <summary>
        /// Add a new translation in the languages dictionaries
        /// </summary>
        /// <param name="textId">The text to translate identifier</param>
        /// <param name="languageId">The language identifier of the translation</param>
        /// <param name="value">The value of the translated text</param>
        public void AddTranslation(string textId, string languageId, string value, string source = "")
        {
            if (!tmInstance.TranslationsDictionary.ContainsKey(textId))
                tmInstance.TranslationsDictionary[textId] = new SortedDictionary<string, TMTranslation>();

            if (!tmInstance.AvailableLanguages.Contains(languageId))
                tmInstance.AvailableLanguages.Add(languageId);

            tmInstance.TranslationsDictionary[textId][languageId] = new TMTranslation()
            {
                TextId = textId,
                LanguageId = languageId,
                TranslatedText = value,
                Source = source
            };
        }
         
        /// <summary>
        /// Load the specified file in the Languages dictionnaries
        /// </summary>
        /// <param name="fileName">The filename of the file to load</param>
        public void AddFile(string fileName)
        {
            FileLanguageLoaders.Find(loader => loader.CanLoadFile(fileName))?.LoadFile(fileName, this);
        }

        /// <summary>
        /// Load all the language files of the specified directory in the languages dictionnaries
        /// </summary>
        /// <param name="path">The path of the directory to load</param>
        public void AddDirectory(string path)
        {
            Directory.GetFiles(path).ToList()
                .ForEach(delegate (string fileName)
                {
                    AddFile(fileName);
                });
        }

        /// <summary>
        /// Remove all translation that comes from the specified source
        /// </summary>
        /// <param name="source">The fileName or source of the translation</param>
        public void RemoveAllFromSource(string source)
        {
            tmInstance.TranslationsDictionary.Keys.ToList().ForEach(textId =>
            {
                tmInstance.TranslationsDictionary[textId].Values.ToList().ForEach(translation =>
                {
                    if(translation.Source.Equals(source))
                    {
                        tmInstance.TranslationsDictionary[textId].Remove(translation.LanguageId);
                    }
                });

                if(tmInstance.TranslationsDictionary[textId].Count == 0)
                {
                    tmInstance.TranslationsDictionary.Remove(textId);
                }
            });
        }

        /// <summary>
        /// Empty All Dictionnaries
        /// </summary>
        public void ClearAllTranslations()
        {
            TM.Instance.TranslationsDictionary.Clear();
            TM.Instance.AvailableLanguages.Clear();
            TM.Instance.MissingTranslations.Clear();
        }
    }
}
