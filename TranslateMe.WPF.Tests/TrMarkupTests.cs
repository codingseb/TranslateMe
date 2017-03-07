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
    public class TrMarkupTests
    {
        [Test]
        public void SimpleTrMarkupProvidingDefaultValue()
        {
            Tr tr = new Tr("Test");

            tr.ProvideValue(new ServiceProviderForTests()).ToString().Equals("Test");
        }
    }
}
