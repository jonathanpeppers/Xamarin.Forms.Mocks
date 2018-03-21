using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Xamarin.Forms.Mocks.Tests
{
    [TestFixture]
    public class DeviceTests
    {
        [Test]
        public void BeginInvokeOnMainThreadIsSynchronous()
        {
            MockForms.Init();

            bool success = false;
            Device.BeginInvokeOnMainThread(() => success = true);
            Assert.IsTrue(success);
        }

        [Test]
        public void RuntimePlatformDefault()
        {
            MockForms.Init();

            Assert.AreEqual("Test", Device.RuntimePlatform);
        }

        [Test]
        public void RuntimePlatformiOS()
        {
            MockForms.Init(Device.iOS);

            Assert.AreEqual(Device.iOS, Device.RuntimePlatform);
            Assert.AreEqual(TargetPlatform.iOS, Device.OS);
        }

        [Test]
        public void RuntimePlatformAndroid()
        {
            MockForms.Init(Device.Android);

            Assert.AreEqual(Device.Android, Device.RuntimePlatform);
            Assert.AreEqual(TargetPlatform.Android, Device.OS);
        }

        [Test]
        public void RuntimePlatformGTK()
        {
            MockForms.Init(Device.GTK);

            Assert.AreEqual(Device.GTK, Device.RuntimePlatform);
        }

        [Test]
        public void RuntimePlatformMacOS()
        {
            MockForms.Init(Device.macOS);

            Assert.AreEqual(Device.macOS, Device.RuntimePlatform);
        }

        [Test]
        public void RuntimePlatformTizen()
        {
            MockForms.Init(Device.Tizen);

            Assert.AreEqual(Device.Tizen, Device.RuntimePlatform);
        }

        [Test]
        public void RuntimePlatformUWP()
        {
            MockForms.Init(Device.UWP);

            Assert.AreEqual(Device.UWP, Device.RuntimePlatform);
        }

        [Test]
        public void RuntimePlatformWPF()
        {
            MockForms.Init(Device.WPF);

            Assert.AreEqual(Device.WPF, Device.RuntimePlatform);
        }

        [Test]
        public void OpenUri()
        {
            MockForms.Init();

            Uri actual = null;
            int callCount = 0;

            MockForms.OpenUriAction = u =>
            {
                actual = u;
                callCount++;
            };

            var expectedUri = new Uri("https://www.google.com");

            Device.OpenUri(expectedUri);

            Assert.AreEqual(expectedUri, actual);
            Assert.AreEqual(1, callCount);
        }

        [Test]
        public void InitClearsOpenUri()
        {
            MockForms.OpenUriAction = delegate { };
            MockForms.Init();
            Assert.IsNull(MockForms.OpenUriAction);
        }

        [Test]
        public void OpenUriDoesNotThrowOnNull()
        {
            MockForms.Init();
            MockForms.OpenUriAction = null;
            Device.OpenUri(new Uri("https://www.google.com"));
        }
        [Test]
        public void IdiomDesktop()
        {
            MockForms.Init(idiom: TargetIdiom.Desktop);

            Assert.AreEqual(TargetIdiom.Desktop, Device.Idiom);
        }

        [Test]
        public void IdiomPhone()
        {
            MockForms.Init(idiom: TargetIdiom.Phone);

            Assert.AreEqual(TargetIdiom.Phone, Device.Idiom);
        }

        [Test]
        public void IdiomTablet()
        {
            MockForms.Init(idiom: TargetIdiom.Tablet);

            Assert.AreEqual(TargetIdiom.Tablet, Device.Idiom);
        }

        [Test]
        public void IdiomTV()
        {
            MockForms.Init(idiom: TargetIdiom.TV);

            Assert.AreEqual(TargetIdiom.TV, Device.Idiom);
        }

        [Test]
        public void IdiomUnsupported()
        {
            MockForms.Init();

            Assert.AreEqual(TargetIdiom.Unsupported, Device.Idiom);
        }

        [Test]
        public async Task StartTimer()
        {
            MockForms.Init();

            var source = new TaskCompletionSource<bool>();
            Device.StartTimer(TimeSpan.FromMilliseconds(1), () =>
            {
                source.SetResult(true);
                return false;
            });

            Assert.IsTrue(await source.Task);
        }

        [Test]
        public async Task StartTimerRepeating()
        {
            MockForms.Init();

            const int max = 10;
            int count = 0;
            var source = new TaskCompletionSource<int>();
            Device.StartTimer(TimeSpan.FromMilliseconds(1), () =>
            {
                if (++count == max)
                {
                    source.SetResult(count);
                    return false;
                }

                return true;
            });

            Assert.AreEqual(max, await source.Task);
        }
    }
}
