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

            var platformColor = new OnPlatform<Color> { Default = Color.Blue };
            platformColor.Platforms.Add(new On
            {
                Value = Color.Red,
                Platform = new[] { Device.iOS }
            });

            Assert.AreNotEqual(Color.Blue, (Color)platformColor);
            Assert.AreEqual(Color.Red, (Color)platformColor);
        }

        [Test]
        public void AndroidColor()
        {
            MockForms.Init(Device.Android);

            var platformColor = new OnPlatform<Color> { Default = Color.Blue };
            platformColor.Platforms.Add(new On
            {
                Value = Color.Red,
                Platform = new[] { Device.Android }
            });

            Assert.AreNotEqual(Color.Blue, (Color)platformColor);
            Assert.AreEqual(Color.Red, (Color)platformColor);
        }

        [Test]
        public void GTKColor()
        {
            MockForms.Init(Device.GTK);

            var platformColor = new OnPlatform<Color> { Default = Color.Blue };
            platformColor.Platforms.Add(new On
            {
                Value = Color.Red,
                Platform = new[] { Device.GTK }
            });

            Assert.AreNotEqual(Color.Blue, (Color)platformColor);
            Assert.AreEqual(Color.Red, (Color)platformColor);
        }

        [Test]
        public void macOSColor()
        {
            MockForms.Init(Device.macOS);

            var platformColor = new OnPlatform<Color> { Default = Color.Blue };
            platformColor.Platforms.Add(new On
            {
                Value = Color.Red,
                Platform = new[] { Device.macOS }
            });

            Assert.AreNotEqual(Color.Blue, (Color)platformColor);
            Assert.AreEqual(Color.Red, (Color)platformColor);
        }

        [Test]
        public void UWPColor()
        {
            MockForms.Init(Device.UWP);

            var platformColor = new OnPlatform<Color> { Default = Color.Blue };
            platformColor.Platforms.Add(new On
            {
                Value = Color.Red,
                Platform = new[] { Device.UWP }
            });

            Assert.AreNotEqual(Color.Blue, (Color)platformColor);
            Assert.AreEqual(Color.Red, (Color)platformColor);
        }

        [Test]
        public void WPFColor()
        {
            MockForms.Init(Device.WPF);

            var platformColor = new OnPlatform<Color> { Default = Color.Blue };
            platformColor.Platforms.Add(new On
            {
                Value = Color.Red,
                Platform = new[] { Device.WPF }
            });

            Assert.AreNotEqual(Color.Blue, (Color)platformColor);
            Assert.AreEqual(Color.Red, (Color)platformColor);
        }
    }
}
