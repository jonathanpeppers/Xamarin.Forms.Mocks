using NUnit.Framework;
using Xamarin.Forms.Xaml;

namespace Xamarin.Forms.Mocks.Tests
{
    [TestFixture]
    public class LoadFromXamlTests
    {
        [SetUp]
        public void SetUp()
        {
            MockForms.Init();
        }

        [Test]
        public void TestCase()
        {
            var label = new Label();
            label.LoadFromXaml("<Label Text=\"Woot\" />");
            Assert.AreEqual("Woot", label.Text);
        }
    }
}
