using System.Threading.Tasks;
using NUnit.Framework;

namespace Xamarin.Forms.Mocks.Tests
{
    [TestFixture]
    public class PropertiesTests
    {
        [SetUp]
        public void SetUp()
        {
            MockForms.Init();
        }

        class App : Application { }

        [Test]
        public async Task SaveDoesNotThrow()
        {
            await new App().SavePropertiesAsync();
        }

        [Test]
        public async Task SaveAndLoad()
        {
            var app = new App();
            app.Properties["Chuck"] = "Norris";
            await app.SavePropertiesAsync();

            app = new App();
            Assert.AreEqual("Norris", app.Properties["Chuck"]);
        }
    }
}
