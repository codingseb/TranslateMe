using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TranslateMe.Examples
{
    public static class Languages
    {
        private static string languagesFilesDirectory = Path.GetFullPath(Path.Combine(
                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                    @"..\..\Translations"
                ));

        public static void Init()
        {
            TM.Instance.LogOutMissingTranslations = true;
            string exampleFileFileName = Path.Combine(languagesFilesDirectory, "Example1.tm.json");
            TMLanguagesLoader.AddFile(exampleFileFileName);
        }
    }
}
