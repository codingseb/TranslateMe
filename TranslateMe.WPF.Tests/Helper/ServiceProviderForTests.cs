using System;

namespace TranslateMe.WPF.Tests
{
    internal class ServiceProviderForTests : IServiceProvider
    {
        public object GetService(Type serviceType)
        {
            return Activator.CreateInstance(serviceType);
        }
    }
}
