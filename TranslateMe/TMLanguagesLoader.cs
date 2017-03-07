using System.IO;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace TranslateMe
{
    public static class TMLanguagesLoader
    {
        /// <summary>
        /// Add a new translation in the languages dictionaries
        /// </summary>
        /// <param name="textId">The text to translate identifier</param>
        /// <param name="languageId">The language identifier of the translation</param>
        /// <param name="value">The value of the translated text</param>
        public static void AddTranslation(string textId, string languageId, string value)
        {
            if (!TM.Instance.TranslationsDictionary.ContainsKey(textId))
                TM.Instance.TranslationsDictionary[textId] = new SortedDictionary<string, string>();

            if (!TM.Instance.AvailableLanguages.Contains(languageId))
                TM.Instance.AvailableLanguages.Add(languageId);

            TM.Instance.TranslationsDictionary[textId][languageId] = value;
        }
         
        /// <summary>
        /// Load the specified file in the Languages dictionnaries
        /// </summary>
        /// <param name="fileName">The filename of the file to load</param>
        public static void AddFile(string filename)
        {
            string json = File.ReadAllText(filename);

            SortedDictionary<string, SortedDictionary<string, string>> fileDictionnary =
                JsonConvert.DeserializeObject<SortedDictionary<string, SortedDictionary<string, string>>>(json);

            fileDictionnary.Keys.ToList().ForEach(delegate (string textId)
            {
                fileDictionnary[textId].Keys.ToList().ForEach(delegate (string languageId)
                {
                    AddTranslation(textId, languageId,  fileDictionnary[textId][languageId]);
                });
            });
        }

        /// <summary>
        /// Load all the language files of the specified directory in the languages dictionnaries
        /// </summary>
        /// <param name="path">The path of the directory to load</param>
        public static void AddDirectory(string path)
        {
            Directory.GetFiles(path, "*.tm.json").ToList()
                .ForEach(delegate (string fileName)
                {
                    AddFile(fileName);
                });
        }
    }
}
