using NUnit.Framework;
using Should;


namespace TranslateMe.WPF.Tests
{
    [TestFixture]
    public class TranslateMeWPFTests
    {
        [OneTimeSetUp]
        public void LoadTranslations()
        {
            TMLanguagesLoader loader = new TMLanguagesLoader(TM.Instance);

            loader.AddTranslation("LanguageName", "en", "English");
            loader.AddTranslation("LanguageName", "fr", "Français");
        }

        //[Category("Markup")]
        //[Test]
        //public void TrMarkupTests()
        //{
        //    Tr tr = new Tr("Test");

        //    tr.ProvideValue(new ServiceProviderForTests()).ToString().ShouldEqual("Test");
        //}

        [Category("Converter")]
        [Test]
        public void TrLanguageIdConverterTests()
        {
            TrLanguageIdConverter converter = new TrLanguageIdConverter();

            converter.DefaultText = "DefaultText";
            converter.TextId = "LanguageName";

            converter.Convert("en", null, null, null).ShouldEqual("English");
            converter.Convert("fr", null, null, null).ShouldEqual("Français");
            converter.Convert("es", null, null, null).ShouldEqual("DefaultText");
        }
    }
}
