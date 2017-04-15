using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Xamarin.Forms.Mocks
{
    public static class MockForms
    {
        public static void Init()
        {
            Device.PlatformServices = new PlatformServices();
            DependencyService.Register<SystemResourcesProvider>();
            DependencyService.Register<Serializer>();
        }

        private class TestTicker : Ticker
        {
            private bool _enabled;

            protected override void EnableTimer()
            {
                _enabled = true;

                while (_enabled)
                {
                    SendSignals(16);
                }
            }

            protected override void DisableTimer()
            {
                _enabled = false;
            }
        }

        private class PlatformServices : IPlatformServices
        {
            public bool IsInvokeRequired
            {
                get { return false; }
            }

            public string RuntimePlatform
            {
                get { return "Test"; }
            }

            public void BeginInvokeOnMainThread(Action action)
            {
                action();
            }

            public Ticker CreateTicker()
            {
                return new TestTicker();
            }

            public Assembly[] GetAssemblies()
            {
                return new Assembly[0];
            }

            public string GetMD5Hash(string input)
            {
                throw new NotImplementedException();
            }

            public double GetNamedSize(NamedSize size, Type targetElementType, bool useOldSizes)
            {
                return 14;
            }

            public Task<Stream> GetStreamAsync(Uri uri, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }

            public IIsolatedStorageFile GetUserStoreForApplication()
            {
                throw new NotImplementedException();
            }

            public void OpenUriAction(Uri uri) { }

            public void StartTimer(TimeSpan interval, Func<bool> callback) { }
        }

        private class SystemResourcesProvider : ISystemResourcesProvider
        {
            private ResourceDictionary _dictionary = new ResourceDictionary();

            public IResourceDictionary GetSystemResources()
            {
                return _dictionary;
            }
        }

        private class Serializer : IDeserializer
        {
            private IDictionary<string, object> _properties = new Dictionary<string, object>();

            public Task<IDictionary<string, object>> DeserializePropertiesAsync()
            {
                return Task.FromResult(_properties);
            }

            public Task SerializePropertiesAsync(IDictionary<string, object> properties)
            {
                _properties = properties;
                return Task.FromResult(true);
            }
        }
    }
}
