using NUnit.Framework;

namespace Xamarin.Forms.Mocks.Tests
{
    [TestFixture]
    public class OnPlatformTests
    {
        [Test]
        public void DefaultString()
        {
            MockForms.Init();

            var platformString = new OnPlatform<string>();
            platformString.Platforms.Add(new On
            {
                Value = "Chuck",
                Platform = new[] { Device.RuntimePlatform }
            });

            Assert.AreEqual("Chuck", (string)platformString);
        }

        [Test]
        public void DefaultColor()
        {
            MockForms.Init();

            var platformColor = new OnPlatform<Color>();
            platformColor.Platforms.Add(new On
            {
                Value = Color.Red,
                Platform = new[] { Device.RuntimePlatform }
            });

            Assert.AreEqual(Color.Red, (Color)platformColor);
        }

        [Test]
        public void iOSColor()
        {
            MockForms.Init(Device.iOS);

            var platformColor = new OnPlatform<Color>();
            platformColor.Platforms.Add(new On
            {
                Value = Color.Red,
                Platform = new[] { Device.iOS }
            });

            Assert.AreEqual(Color.Red, (Color)platformColor);
        }

        [Test]
        public void AndroidColor()
        {
            MockForms.Init(Device.Android);

            var platformColor = new OnPlatform<Color>();
            platformColor.Platforms.Add(new On
            {
                Value = Color.Red,
                Platform = new[] { Device.Android }
            });

            Assert.AreEqual(Color.Red, (Color)platformColor);
        }

        [Test]
        public void WindowsColor()
        {
            MockForms.Init(Device.WinRT);

            var platformColor = new OnPlatform<Color>();
            platformColor.Platforms.Add(new On
            {
                Value = Color.Red,
                Platform = new[] { Device.WinRT }
            });

            Assert.AreEqual(Color.Red, (Color)platformColor);
        }

        [Test]
        public void WinPhoneColor()
        {
            MockForms.Init(Device.WinPhone);

            var platformColor = new OnPlatform<Color>();
            platformColor.Platforms.Add(new On
            {
                Value = Color.Red,
                Platform = new[] { Device.WinPhone }
            });

            Assert.AreEqual(Color.Red, (Color)platformColor);
        }
    }
}
