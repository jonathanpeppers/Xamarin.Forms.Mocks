using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Internals;

namespace Xamarin.Forms.Mocks
{
    internal class SystemResourcesProvider : ISystemResourcesProvider
    {
        private ResourceDictionary _dictionary = new ResourceDictionary();

        public IResourceDictionary GetSystemResources()
        {
            return _dictionary;
        }
    }
}
