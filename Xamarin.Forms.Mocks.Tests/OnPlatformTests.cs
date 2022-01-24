using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
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
                Value = Colors.Red,
                Platform = new[] { Device.RuntimePlatform }
            });

            Assert.AreEqual(Colors.Red, (Color)platformColor);
        }

        [Test]
        public void iOSColor()
        {
            MockForms.Init(Device.iOS);

            var platformColor = new OnPlatform<Color> { Default = Colors.Blue };
            platformColor.Platforms.Add(new On
            {
                Value = Colors.Red,
                Platform = new[] { Device.iOS }
            });

            Assert.AreNotEqual(Colors.Blue, (Color)platformColor);
            Assert.AreEqual(Colors.Red, (Color)platformColor);
        }

        [Test]
        public void AndroidColor()
        {
            MockForms.Init(Device.Android);

            var platformColor = new OnPlatform<Color> { Default = Colors.Blue };
            platformColor.Platforms.Add(new On
            {
                Value = Colors.Red,
                Platform = new[] { Device.Android }
            });

            Assert.AreNotEqual(Colors.Blue, (Color)platformColor);
            Assert.AreEqual(Colors.Red, (Color)platformColor);
        }

        [Test]
        public void GTKColor()
        {
            MockForms.Init(Device.GTK);

            var platformColor = new OnPlatform<Color> { Default = Colors.Blue };
            platformColor.Platforms.Add(new On
            {
                Value = Colors.Red,
                Platform = new[] { Device.GTK }
            });

            Assert.AreNotEqual(Colors.Blue, (Color)platformColor);
            Assert.AreEqual(Colors.Red, (Color)platformColor);
        }

        [Test]
        public void macOSColor()
        {
            MockForms.Init(Device.macOS);

            var platformColor = new OnPlatform<Color> { Default = Colors.Blue };
            platformColor.Platforms.Add(new On
            {
                Value = Colors.Red,
                Platform = new[] { Device.macOS }
            });

            Assert.AreNotEqual(Colors.Blue, (Color)platformColor);
            Assert.AreEqual(Colors.Red, (Color)platformColor);
        }

        [Test]
        public void UWPColor()
        {
            MockForms.Init(Device.UWP);

            var platformColor = new OnPlatform<Color> { Default = Colors.Blue };
            platformColor.Platforms.Add(new On
            {
                Value = Colors.Red,
                Platform = new[] { Device.UWP }
            });

            Assert.AreNotEqual(Colors.Blue, (Color)platformColor);
            Assert.AreEqual(Colors.Red, (Color)platformColor);
        }

        [Test]
        public void WPFColor()
        {
            MockForms.Init(Device.WPF);

            var platformColor = new OnPlatform<Color> { Default = Colors.Blue };
            platformColor.Platforms.Add(new On
            {
                Value = Colors.Red,
                Platform = new[] { Device.WPF }
            });

            Assert.AreNotEqual(Colors.Blue, (Color)platformColor);
            Assert.AreEqual(Colors.Red, (Color)platformColor);
        }
    }
}
