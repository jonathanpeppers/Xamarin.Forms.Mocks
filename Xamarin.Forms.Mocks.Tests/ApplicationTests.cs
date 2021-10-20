using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using NUnit.Framework;

namespace Xamarin.Forms.Mocks.Tests
{
    [TestFixture]
    public class ApplicationTests
    {
        [SetUp]
        public void SetUp()
        {
            MockForms.Init();
        }

        [TearDown]
        public void TearDown()
        {
            Application.Current = null;
        }

        class CustomApplication : Application
        {
            public CustomApplication()
            {
                Resources = new ResourceDictionary();
                Resources["Chuck"] = "Norris";
            }
        }

        [Test]
        public void ApplicationFromCode()
        {
            Application.Current = new CustomApplication();

            Assert.AreEqual(Application.Current.Resources["Chuck"], "Norris");
        }

        [Test]
        public void ApplicationFromXaml()
        {
            Application.Current = new App();

            Assert.AreEqual(Application.Current.Resources["White"], Colors.White);
        }
    }
}
