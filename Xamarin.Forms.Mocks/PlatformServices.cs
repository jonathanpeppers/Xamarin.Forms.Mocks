using Microsoft.Maui.Animations;
using Microsoft.Maui.Controls;
using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Maui.Controls.Internals;
using Microsoft.Maui;
using Microsoft.Maui.Graphics;

namespace Xamarin.Forms.Mocks
{
    internal class PlatformServices : IPlatformServices
    {
        private readonly IsolatedStorageFile _isolatedStorageFile = new IsolatedStorageFile();

        public PlatformServices(string runtimePlatform, OSAppTheme requestedTheme)
        {
            RuntimePlatform = runtimePlatform;
            RequestedTheme = requestedTheme;
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

        public OSAppTheme RequestedTheme { get; }

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

        public string GetHash(string input)
        {
            var stringBuilder = new StringBuilder();
            using (var hash = SHA256.Create())
            {
                var enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(input));
                foreach (byte b in result)
                    stringBuilder.Append(b.ToString("x2"));
            }

            return stringBuilder.ToString();
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

        public Color GetNamedColor(string name)
        {
            // Not supported on this platform
            return Colors.Black;
        }
    }
}
