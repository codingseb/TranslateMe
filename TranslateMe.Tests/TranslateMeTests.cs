using Should;
using NUnit.Framework;

namespace TranslateMe.Tests
{
    [TestFixture]
    public class TranslateMeTests
    {
        TMLanguagesLoader loader;

        [OneTimeSetUp]
        public void LoadTranslations()
        {
            loader = new TMLanguagesLoader(TM.Instance);

            loader.AddTranslation("SayHello", "en", "Hello");
            loader.AddTranslation("SayHello", "fr", "Bonjour");
        }

        [TestCase("TestNoTextId", "Test", null, null, ExpectedResult = "Test")]
        [TestCase("SayHello", "SH", null, null, ExpectedResult = "Hello")]
        [TestCase("SayHello", "SH", "fr", null, ExpectedResult = "Bonjour")]
        [TestCase("SayHello", "SH", "fr", "en", ExpectedResult = "Hello")]
        [TestCase("SayHello", "SH", "fr", "es", ExpectedResult = "SH")]
        public string StaticBasicTranslations(string textId, string defaultText, string currentLanguage, string forceCurrentLanguage)
        {
            if(currentLanguage != null)
                TM.Instance.CurrentLanguage = currentLanguage;

            if (forceCurrentLanguage != null)
            {
                return TM.Tr(textId, defaultText, forceCurrentLanguage);
            }
            else
            {
                return TM.Tr(textId, defaultText);
            }
        }

        [OneTimeTearDown]
        public void ClearDicts()
        {
            loader.ClearAllTranslations();
        }
    }
}
