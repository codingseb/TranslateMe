using System.IO;
using System.Reflection;

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
            TMLanguagesLoader loader = new TMLanguagesLoader(TM.Instance);

            loader.AddFile(exampleFileFileName);
        }
    }
}
