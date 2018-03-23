using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TranslateMe
{
    public class TMJsonFileLanguageLoader : ITMFileLanguageLoader
    {
        public bool CanLoadFile(string fileName)
        {
            return fileName.ToLower().EndsWith(".tm.json");
        }

        public void LoadFile(string fileName, TMLanguagesLoader mainLoader)
        {
            string json = File.ReadAllText(fileName);

            SortedDictionary<string, SortedDictionary<string, string>> fileDictionnary =
                JsonConvert.DeserializeObject<SortedDictionary<string, SortedDictionary<string, string>>>(json);

            fileDictionnary.Keys.ToList().ForEach(delegate (string textId)
            {
                fileDictionnary[textId].Keys.ToList().ForEach(delegate (string languageId)
                {
                    mainLoader.AddTranslation(textId, languageId, fileDictionnary[textId][languageId]);
                });
            });
        }
    }
}
