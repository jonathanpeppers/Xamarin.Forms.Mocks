using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Xamarin.Forms;

namespace Xamarin.Forms.Mocks.Tests
{
    [TestFixture]
    public class AppThemeTests
    {
        [Test]
        public void DefaultAppThemeIsUnspecified()
        {
            MockForms.Init();
            var str = new OnAppTheme<string>
            {
                Default = "Unspecified",
                Dark = "Dark",
                Light = "Light"
            };
            Assert.AreEqual("Unspecified", str.Value);
        }

        [Test]
        public void RequestedThemeIsSet()
        {
            MockForms.Init(requestedTheme: OSAppTheme.Dark);
            var str = new OnAppTheme<string>
            {
                Default = "Unspecified",
                Dark = "Dark",
                Light = "Light"
            };
            Assert.AreEqual("Dark", str.Value);
        }
    }
}