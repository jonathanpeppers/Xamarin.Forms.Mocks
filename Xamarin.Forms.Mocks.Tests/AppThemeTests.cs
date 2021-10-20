using System;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
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

            var app = new Application();
            Assert.AreEqual(OSAppTheme.Unspecified, app.RequestedTheme);
        }

        [Test]
        public void RequestedThemeIsSet()
        {
            MockForms.Init(requestedTheme: OSAppTheme.Dark);
            var app = new Application();
            Assert.AreEqual(OSAppTheme.Dark, app.RequestedTheme);
        }
    }
}