using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            TMLanguagesLoader.AddTranslation("LanguageName", "en", "English");
            TMLanguagesLoader.AddTranslation("LanguageName", "fr", "Français");
        }

        [Category("Markup")]
        [Test]
        public void TrMarkupTests()
        {
            Tr tr = new Tr("Test");

            tr.ProvideValue(new ServiceProviderForTests()).ToString().ShouldEqual("Test");
        }

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
