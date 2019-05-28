using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;

namespace Xamarin.Forms.Mocks
{
    internal class PlatformServices : IPlatformServices
    {
        private readonly IsolatedStorageFile _isolatedStorageFile = new IsolatedStorageFile();

        public PlatformServices(string runtimePlatform)
        {
            RuntimePlatform = runtimePlatform;
        }

        public bool IsInvokeRequired
        {
            get { return false; }
        }

        public string RuntimePlatform
        {
            get;
            private set;
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
            return AppDomain.CurrentDomain.GetAssemblies ();
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
            return _isolatedStorageFile;
        }

        public void OpenUriAction(Uri uri)
        {
            MockForms.OpenUriAction?.Invoke(uri);
        }

        public void QuitApplication() { }

        public SizeRequest GetNativeSize(VisualElement view, double widthConstraint, double heightConstraint)
        {
            return new SizeRequest();
        }

        public async void StartTimer(TimeSpan interval, Func<bool> callback)
        {
            while (true)
            {
                await Task.Delay(interval);

                if (!callback())
                    return;
            }
        }
    }
}
